using System.Collections;
using Features.Bubble_Namespace.Scripts;
using UnityEngine;

namespace Features.Interactables_Namespace.Scripts
{
    public class AntiVirusBehaviour : MonoBehaviour
    {   
    
        [SerializeField] private float activeAntivirusDuration = 5f;
    
        private void OnCollisionEnter(Collision collision)
        {
            GameObject collidedObject = collision.gameObject;
            // if collided object is bubble and antiVirusEnabled is false (to prevent stacking more than one antiVirus)
            if (collidedObject.CompareTag("Bubble") && !collidedObject.GetComponent<BubbleBehaviour>().antiVirusEnabled)
            {
                StartCoroutine(SetTemporaryAntiVirus(collidedObject));
            }
        }

        private IEnumerator SetTemporaryAntiVirus(GameObject bubble)
        {
            if (!bubble.GetComponent<BubbleBehaviour>().antiVirusEnabled)
            {
                bubble.GetComponent<BubbleBehaviour>().antiVirusEnabled = true;
                Debug.Log("Enable antiVirus");
                
                yield return new WaitForSeconds(activeAntivirusDuration);
                
                Debug.Log("Disable antiVirus");
                bubble.GetComponent<BubbleBehaviour>().antiVirusEnabled = false;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    
    
    }
}
