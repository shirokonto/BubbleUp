using DataStructures.Variables;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Features.Bubble_Namespace.Scripts
{
    public class BubbleBehaviour : MonoBehaviour
    {
        [SerializeField] private string correctInfoType; //will be set before the game starts via character view
        [SerializeField] private BoolVariable bubbleIsPopped; 
        private const float BUBBLE_SCALING = 0.03f;
        public ParticleSystem bubblePop;
        private Vector3 _scaleChange;
        private int _hit = 0;
        public bool adBlockerEnabled;

        private void Start()
        {
            _scaleChange = new Vector3(BUBBLE_SCALING, BUBBLE_SCALING, BUBBLE_SCALING);
            bubbleIsPopped.Set(false);
        }
    
        private void OnCollisionEnter(Collision other)
        {
            switch (other.gameObject.tag)
            {
                case "InfoObject":
                    Debug.Log("Hit me");
                    HitInfoItem(other.gameObject);
                    break; ;
                case "MinimizeBubble":
                    HitMinimizeBubble(other.gameObject);
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
