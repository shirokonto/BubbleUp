using UnityEngine;

namespace Features.Interactables_Namespace.Scripts
{
    public class VirusBehaviour : MonoBehaviour
    {
    
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Bubble"))
            {
            
            }
        }

    }
}
