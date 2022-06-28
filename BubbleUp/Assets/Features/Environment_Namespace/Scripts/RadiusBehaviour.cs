using System.Collections.Generic;
using UnityEngine;

namespace Features.Environment_Namespace.Scripts
{
    public class RadiusBehaviour : MonoBehaviour
    {

        [SerializeField] private List<GameObject> collidableItems;
        // Start is called before the first frame update
        void Start()
        {
            foreach (GameObject item in collidableItems)
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), item.GetComponent<Collider>());
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), other.gameObject.GetComponent<Collider>());
        }

        private void OnCollisionExit(Collision other)
        {
            other.gameObject.SetActive(false);
        }
    }
}
