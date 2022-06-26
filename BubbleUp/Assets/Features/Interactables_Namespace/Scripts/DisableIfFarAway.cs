using System;
using UnityEngine;

namespace Features.Interactables_Namespace.Scripts
{
    public class DisableIfFarAway : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private float distanceFromPlayer;
        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Vector3.Distance(player.position, gameObject.transform.position) > distanceFromPlayer)
            {
                Debug.Log("Bye");
                gameObject.SetActive(false);
                _rigidbody.velocity = Vector3.zero;
            }

        }
    }
}
