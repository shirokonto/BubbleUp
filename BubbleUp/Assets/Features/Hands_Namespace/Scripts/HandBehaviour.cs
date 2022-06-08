using System.Collections;
using Leap.Unity.Interaction;
using UnityEngine;
using static Leap.Unity.Interaction.InteractionControllerSet;

namespace Features.Hands_Namespace.Scripts
{
    public class HandBehaviour : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("InfoObject"))
            {
                Debug.Log("Touched left hand");
                collision.gameObject.GetComponent<Collider>().enabled = false;
                StartCoroutine(DeactivateGameObject(10, collision));
            }
        }
    
        private IEnumerator DeactivateGameObject(float duration, Collision collision) {
            yield return new WaitForSeconds(duration);
            collision.gameObject.SetActive(false);
        }
        
    }
}
