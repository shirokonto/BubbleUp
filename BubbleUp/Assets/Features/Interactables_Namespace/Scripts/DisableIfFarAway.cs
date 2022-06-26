using UnityEngine;

namespace Features.Interactables_Namespace.Scripts
{
    public class DisableIfFarAway : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private float distanceFromPlayer;

        // Update is called once per frame
        void Update()
        {
            if (Vector3.Distance(player.position, gameObject.transform.position) > distanceFromPlayer)
            {
                Debug.Log("Bye");
                gameObject.SetActive(false);
            }

        }
    }
}
