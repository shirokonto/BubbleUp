using System;
using UnityEngine;

namespace Features.Interactables_Namespace.Scripts
{
    public class DisableIfFarAwayOrHitBubble : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private float distanceFromPlayer;
        private Rigidbody _rigidbody;
        private Transform _transform;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Vector3.Distance(player.position, gameObject.transform.position) > distanceFromPlayer)
            {
                ResetPositionAndRotation();
            }

        }

        public void ResetPositionAndRotation()
        {
            gameObject.SetActive(false);
            _rigidbody.velocity = Vector3.zero;
            _transform.rotation = Quaternion.Euler(Vector3.zero);
            _rigidbody.freezeRotation = true;
        }
    }
}
