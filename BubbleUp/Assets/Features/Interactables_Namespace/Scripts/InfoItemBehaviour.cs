using UnityEngine;

namespace Features.Interactables_Namespace.Scripts
{
    public class InfoItemBehaviour : MonoBehaviour
    {
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("Touched left hand");
                
            }
        }
    }
}