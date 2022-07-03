using System.Collections;
using DataStructures.Variables;
using Features.Interactables_Namespace.Scripts;
using Features.Menu_Namespace.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Event_Namespace;
using Vector3 = UnityEngine.Vector3;

namespace Features.Bubble_Namespace.Scripts
{
    public class BubbleBehaviour : MonoBehaviour {
        [SerializeField] private string correctInfoType; //will be set before the game starts via character view
        [SerializeField] private BoolVariable bubbleIsPopped;
        [SerializeField] private GameEvent showPopup;
        [SerializeField] private IntVariable points;
        public bool adBlockerEnabled;        
        public ParticleSystem bubblePop;
        private Vector3 _scaleChange;
        private int _hit = 0;
        private int localPoints = 0;
        private const float BUBBLE_SCALING = 0.03f;
        private const int PLUSPOINT = 1;
        private const int MINUSPOINTS = 3;
        

        private void Start()
        {
            _scaleChange = new Vector3(BUBBLE_SCALING, BUBBLE_SCALING, BUBBLE_SCALING);
            bubbleIsPopped.Set(false);
            bubblePop.Stop();
        }

        private void OnCollisionEnter(Collision other)
        {
            switch (other.gameObject.tag)
            {
                case "InfoObject":
                    HitInfoItem(other.gameObject);
                    break; ;
                case "MinimizeBubble":
                    HitMinimizeBubble(other.gameObject);
                    break;
                case "Virus":
                    HitVirus(other.gameObject);
                    break;
            }
        }

        private void HitInfoItem(GameObject infoItem){
            if (!infoItem.transform.name.Contains(correctInfoType))
            {
                _hit += 1;
                localPoints -= MINUSPOINTS;
                points.Set(localPoints);
                transform.localScale += _scaleChange;
            } else
            {
                localPoints += PLUSPOINT;
                points.Set(localPoints);
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

        public void HitVirus(GameObject virusItem)
        {
            virusItem.GetComponent<ItemMover>().ResetPositionAndRotationBeforeRespawn();
            showPopup.Raise();
            SelectedItem.virus = true;
        }

        public IEnumerator ShowAdWhenMinimizeHit()
        {
            //show Timer item
            SelectedItem.minimize = true;
            yield return new WaitForSeconds(0.3f);
        }
    }
}

