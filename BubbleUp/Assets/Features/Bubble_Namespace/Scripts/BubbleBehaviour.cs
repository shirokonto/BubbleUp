using System.Collections;
using DataStructures.Variables;
using Features.Interactables_Namespace.Scripts;
using Features.Menu_Namespace.Scripts;
using Features.UI_Namespace.Script;
using UnityEngine;
using Utilities.Event_Namespace;
using Vector3 = UnityEngine.Vector3;
using Random = System.Random;

namespace Features.Bubble_Namespace.Scripts
{
    public class BubbleBehaviour : MonoBehaviour {
        [SerializeField] private StringVariable correctInfoType; //will be set before the game starts via character view
        [SerializeField] private BoolVariable bubbleIsPopped;
        [SerializeField] private GameEvent showPopup;
        [SerializeField] private IntVariable points;
        [SerializeField] private BoolVariable adBlockerEnabled; //TODO: add bool variable
        public ParticleSystem bubblePop;
        private Vector3 _scaleChange;
        private int _hit;
        private int _localPoints;
        private const float BUBBLE_SCALING = 0.03f;
        private const int PLUSPOINT = 1;
        private const int MINUSPOINTS = 3;
        private readonly string[] _infoItemNames = new string[5] {"InfoObjectPink", "InfoObjectBlue", "InfoObjectRed", "InfoObjectGreen", "InfoObjectYellow"}; 
        private Random _random;
        //private int antivirusGauge = 0;

        private void Awake()
        {
            _scaleChange = new Vector3(BUBBLE_SCALING, BUBBLE_SCALING, BUBBLE_SCALING);
            _random = new Random();
        }

        private void Start()
        {
            _hit = 0;
            _localPoints = 0;
            bubbleIsPopped.Set(false);
            bubblePop.Stop();
            SetCorrectInfoItemRandomly();
        }

        private void OnCollisionEnter(Collision other)
        {
            switch (other.gameObject.tag)
            {
                case "InfoObject":
                    HitInfoItem(other.gameObject);
                    break;
                case "MinimizeBubble":
                    HitMinimizeBubble(other.gameObject);
                    break;
                case "Virus":
                    HitVirus(other.gameObject);
                    break;
                case "AntiVirus":
                HitAntiVirus(other.gameObject);
                break;
            }
        }

        private void HitInfoItem(GameObject infoItem){
            if (!infoItem.transform.name.Contains(correctInfoType.Get()))
            { 
                _hit += 1; 
                _localPoints = (_localPoints-= MINUSPOINTS) <= 0 ? 0 : (_localPoints);
                points.Set(_localPoints);
                transform.localScale += _scaleChange;
            } else
            {
                _localPoints += PLUSPOINT;
                points.Set(_localPoints);
            }
            if(_hit == 3)
            {
                bubbleIsPopped.Set(true);
                Destroy(this.gameObject);
                bubblePop.Play();
                Menu.isGameOver = true;
            }
            infoItem.GetComponent<ItemMover>().ResetPositionAndRotationBeforeRespawn();
        }
        
        private void HitMinimizeBubble(GameObject minimizeItem)
        {
            if (_hit >= 1 && _hit < 3)
            {
                _hit -= 1;
                transform.localScale -= _scaleChange;
            }
            minimizeItem.GetComponent<ItemMover>().ResetPositionAndRotationBeforeRespawn();
            ShowAdWhenMinimizeHit();
        }

        private void HitVirus(GameObject virusItem)
        {
            //if (antivirusGauge > 0) antivirusGauge -= 1;
            // Debug.Log(antivirusGauge);
            virusItem.GetComponent<ItemMover>().ResetPositionAndRotationBeforeRespawn();
            if (!antiVirusEnabled)
            {
                showPopup.Raise();
                SelectedItem.virus = true;
            }
        }

        private void HitAntiVirus(GameObject antiVirusItem)
        {
            //if(antivirusGauge == 0 || antivirusGauge < 3) antivirusGauge += 1;
            //Debug.Log(antivirusGauge);
            if (!antiVirusEnabled)
            {
                Debug.Log("Collected antivirus, 5 sec shield!");
                antiVirusItem.GetComponent<ItemMover>().ResetPositionAndRotationBeforeRespawn();
                SelectedItem.antiVirus = true;
            }
        }

        private IEnumerator ShowAdWhenMinimizeHit()
        {
            //show Timer item
            SelectedItem.minimize = true;
            yield return new WaitForSeconds(0.3f);
        }
        
        private void SetCorrectInfoItemRandomly()
        {
            int index = _random.Next(_infoItemNames.Length);
            correctInfoType.Set(_infoItemNames[index]);
        }
    }
}

