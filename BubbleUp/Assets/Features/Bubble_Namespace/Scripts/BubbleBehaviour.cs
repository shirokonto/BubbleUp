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
    /// <summary>
    /// Takes care of the behaviour of the bubble such as resizing,
    /// setting an correct infoItem and takes care of the action of the
    /// collided items InfoItem, MinimizeBubble, Virus and AntiVirus
    /// </summary>
    public class BubbleBehaviour : MonoBehaviour {
        [SerializeField] private StringVariable correctInfoType; //will be set before the game starts via character view
        [SerializeField] private BoolVariable bubbleIsPopped;
        [SerializeField] private GameEvent showPopup;
        [SerializeField] private GameEvent onLoadGameOver;
        [SerializeField] private IntVariable points;
        [SerializeField] private BoolVariable antiVirusEnabled;
        [SerializeField] private AudioClip bubblePopSound;
        [SerializeField] private BoolVariable showVirus;
        [SerializeField] private BoolVariable showMinimize;
        public ParticleSystem bubblePop;
        private Vector3 _scaleChange;
        private int _hit;
        private int _localPoints;
        private const float BUBBLE_SCALING = 0.03f;
        private const int PLUS_POINTS = 3;
        private const int MINUS_POINT = 1;
        private readonly string[] _infoItemNames = {"InfoObjectPink", "InfoObjectBlue", "InfoObjectRed", "InfoObjectGreen", "InfoObjectYellow"}; 
        private Random _random;

       
        private void Awake()
        {
            _scaleChange = new Vector3(BUBBLE_SCALING, BUBBLE_SCALING, BUBBLE_SCALING);
            _random = new Random();
        }

        /*
         * Initialize and set variables
         */
        private void Start()
        {
            _hit = 0;
            _localPoints = 0;
            bubbleIsPopped.Set(false);
            antiVirusEnabled.Set(false);
            bubblePop.Stop();
            SetCorrectInfoItemRandomly();
        }

        /*
         * Detects different GameObjects on collision with bubble
         */
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

        /*
         * Points are collected and the localPoints are set if the correct InfoType is collected.
         * Bubble size changes depending on InfoType
         * Bubble bursts if it got hit by wrong InfoTypes three times 
         */
        private void HitInfoItem(GameObject infoItem)
        {
            if (!infoItem.transform.name.Contains(correctInfoType.Get()))
            {
                _hit += 1;
                _localPoints = (_localPoints -= MINUS_POINT) <= 0 ? 0 : (_localPoints);
                points.Set(_localPoints);
                transform.localScale += _scaleChange;
            }
            else
            {
                _localPoints += PLUS_POINTS;
                points.Set(_localPoints);
            }

            if (_hit == 3)
            {
                AudioSource.PlayClipAtPoint(bubblePopSound, transform.position);
                bubbleIsPopped.Set(true);
                Destroy(this.gameObject);
                bubblePop.Play();
                Menu.IsGameOver = true;
                onLoadGameOver.Raise();
            }

            infoItem.GetComponent<ItemMover>().ResetPositionAndRotationBeforeRespawn();
        }

        /**
         * Minimizes Bubble
         */
        private void HitMinimizeBubble(GameObject minimizeItem)
        {
            if (_hit >= 1 && _hit < 3)
            {
                _hit -= 1;
                transform.localScale -= _scaleChange;
            }
            minimizeItem.GetComponent<ItemMover>().ResetPositionAndRotationBeforeRespawn();
            StartCoroutine(ShowIconWhenMinimizeHit());
        }

        /*
         * Popups appear if VirusItem hits the bubble
         */
        private void HitVirus(GameObject virusItem)
        {
            virusItem.GetComponent<ItemMover>().ResetPositionAndRotationBeforeRespawn();
            if (!antiVirusEnabled.Get())
            {
                showPopup.Raise();
                showVirus.Set(true);
            }
        }

        /*
         * Removes the popups if Antivirus hits the bubble
         */
        private void HitAntiVirus(GameObject antiVirusItem)
        {
            if (!antiVirusEnabled.Get())
            { 
                antiVirusEnabled.Set(true);
                antiVirusItem.GetComponent<ItemMover>().ResetPositionAndRotationBeforeRespawn();
            }
            else
            {
                antiVirusItem.GetComponent<ItemMover>().ResetPositionAndRotationBeforeRespawn();
            }
        }
        
        /*
         * Set the icons in the game visible when collected
         */
        private IEnumerator ShowIconWhenMinimizeHit()
        {
            showMinimize.Set(true);
            yield return new WaitForSeconds(0.3f);
            showMinimize.Set(false);
        }
        
        private void SetCorrectInfoItemRandomly()
        {
            int index = _random.Next(_infoItemNames.Length);
            correctInfoType.Set(_infoItemNames[index]);
        }
    }
}

