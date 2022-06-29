using System.Collections;
using System.Collections.Generic;
using DataStructures.Variables;
using Features.Interactables_Namespace.Scripts;
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
        private const float BUBBLE_SCALING = 0.03f;
        public ParticleSystem bubblePop;
        private Vector3 _scaleChange;
        private int _hit = 0;
        public bool adBlockerEnabled;

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
                transform.localScale += _scaleChange;
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
        }

        public void HitVirus(GameObject virusItem)
        {
            virusItem.GetComponent<ItemMover>().ResetPositionAndRotationBeforeRespawn();
            showPopup.Raise();
        }
    }
}
