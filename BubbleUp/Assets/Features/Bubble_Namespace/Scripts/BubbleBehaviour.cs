using System;
using DataStructures.Variables;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Features.Bubble_Namespace.Scripts
{
    public class BubbleBehaviour : MonoBehaviour
    {
        [SerializeField] private string correctInfoType; //will be set before the game starts via character view
        [SerializeField] private ParticleSystem bubblePop;
        [SerializeField] private Animator [] activeCharacters;
        [SerializeField] private BoolVariable bubbleIsPopped; 
        public bool adBlockerEnabled;
        private const float BUBBLE_SCALING = 0.03f;
        private Vector3 _scaleChange;
        private int _hit = 0;

        private void Awake()
        {
            
            _scaleChange = new Vector3(BUBBLE_SCALING, BUBBLE_SCALING, BUBBLE_SCALING);
        }

        private void Start()
        {
            bubbleIsPopped.Set(false);
        }

        private void OnCollisionEnter(Collision collision)
        {
            switch (collision.gameObject.tag)
            {
                case "InfoObject": 
                    HitInfoItem(collision.gameObject);
                    break; ;
                case "MinimizeBubble":
                    HitMinimizeBubble(collision.gameObject);
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
                //Debug.Log("bubble popped in bubblebehaviour: " + bubbleIsPopped.Get());
                bubbleIsPopped.Set(true);
                Destroy(this.gameObject);
                
                bubblePop.Play();
            }
            infoItem.SetActive(false);
        }
    
        //TODO: call in HandBehaviour.cs
        public void HitMinimizeBubble(GameObject minimizeItem)
        {
            if (_hit >= 1 && _hit < 3)
            {
                _hit -= 1;
                transform.localScale -= _scaleChange;
            }
            minimizeItem.GetComponent<SphereCollider>().enabled = false;
            minimizeItem.SetActive(false);
        }
    }
}
