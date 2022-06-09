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

        // private bool IsHand(Collider other)
        // {
        //     if (other.transform.parent && other.transform.parent.parent && other.transform.parent.parent.GetComponent<HandModel>())
        //         return true;
        //     else
        //         return false;
        // }

        // void OnTriggerEnter(Collider other) 
        // {
        //     if (IsHand(other))
        //     {
        //         Debug.Log("Yay! A hand collided!");
        //     }  
        // }
    
