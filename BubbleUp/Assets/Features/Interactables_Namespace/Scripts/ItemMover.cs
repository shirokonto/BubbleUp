using DataStructures.Variables;
using Leap.Unity;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

namespace Features.Interactables_Namespace.Scripts
{
    public class ItemMover : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 0.5f;
        [SerializeField] private GameObject spawner;
        [SerializeField] private BoolVariable isSecondWave;
        [SerializeField] private Transform player;
        [SerializeField] private float distanceFromPlayer;
        private Transform _startPosition;
        private Quaternion _startRotation;
        private Transform _currentPoint;
        private Route _currentRoute;
        private Rigidbody _rigidbody;
        private Transform _transform;
        private ItemMover _itemMover;

        // Start is called before the first frame update
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = GetComponent<Transform>();
            _itemMover = GetComponent<ItemMover>();
            
            //Set initial position to first point of one of the three routes
            if (gameObject.transform.name.Contains("Clone"))
            {
                _currentRoute = spawner.GetComponent<SpawnController>().GetCurrentRoute();
        
                _currentPoint = _currentRoute.GetNextWayPoint(_currentPoint);
                _startPosition = _currentPoint;
                _startRotation = transform.rotation;
                transform.position = _currentPoint.position;
        
                //Set next waypoint target
                _currentPoint = _currentRoute.GetNextWayPoint(_currentPoint);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (gameObject.transform.name.Contains("Clone"))
            {
                if (isSecondWave.Get())
                {
                    moveSpeed = 0.75f;
                }
                MoveItemTowardsBubble();
                if (Vector3.Distance(player.position, gameObject.transform.position) > distanceFromPlayer)
                {
                    ResetPositionAndRotationBeforeRespawn();
                }
            }
        }

        private void MoveItemTowardsBubble()
        {
            var position = _currentPoint.position;
            transform.position =
                Vector3.MoveTowards(transform.position, position, moveSpeed * Time.deltaTime);
        }

        public void ResetPositionAndRotationBeforeRespawn()
        {
            gameObject.SetActive(false);
            
            //reset position
            _transform = _startPosition;
            _rigidbody.velocity = Vector3.zero;
        
            //reset rotation
            _transform.rotation = _startRotation;
            _transform.eulerAngles = Vector3.zero;
            _transform.localRotation = quaternion.Euler(Vector3.zero);
            //_transform.rotation = Quaternion.identity;
            _rigidbody.freezeRotation = true;
        }

        public void ScaleMoveSpeed(float percentage)
        {
            //set only if not 0.75 or 0.5
            moveSpeed = moveSpeed * percentage;
        }

        public float GetMoveSpeed()
        {
            return moveSpeed;
        }
    }
}
