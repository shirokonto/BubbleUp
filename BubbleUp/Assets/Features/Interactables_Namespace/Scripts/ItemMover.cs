using DataStructures.Variables;
using Unity.Mathematics;
using UnityEngine;

namespace Features.Interactables_Namespace.Scripts
{
    public class ItemMover : MonoBehaviour
    {
        [SerializeField] private BoolVariable isSecondWave;
        [SerializeField] private FloatVariable moveSpeed;
        [SerializeField] private Transform player;
        [SerializeField] private float distanceFromPlayer;
        private Transform _startPosition;
        private Quaternion _startRotation;
        private Transform _currentPoint;
        private Route _currentRoute;
        private Rigidbody _rigidbody;
        private Transform _transform;
        private ObjectPool _objectPool;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = GetComponent<Transform>();
        }

        /// Start is called before the first frame update
        private void Start()
        {
            //Set initial position to first point of current route
            if (!gameObject.transform.name.Contains("Clone")) return;
            
            _objectPool = gameObject.transform.GetComponentInParent<ObjectPool>();
            _startPosition = _currentRoute.gameObject.transform.GetChild(0).gameObject.transform; 
            _currentPoint = _currentRoute.GetNextWayPoint(_currentPoint);
            _startRotation = transform.rotation;
            transform.position = _currentPoint.position;
        
            //Set next waypoint target
            _currentPoint = _currentRoute.GetNextWayPoint(_currentPoint);
        }

        /// Update is called once per frame
        private void Update()
        {
            if (gameObject.transform.name.Contains("Clone"))
            {
                if (isSecondWave.Get())
                {
                    moveSpeed.Set(0.7f);
                }

                MoveItemTowardsBubble();
                if (Vector3.Distance(player.position, gameObject.transform.position) > distanceFromPlayer)
                {
                    ResetPositionAndRotationBeforeRespawn();
                }
            }
        }

        /// <summary>
        /// Sets the current route on which the item should spawn
        /// </summary>
        public void SetCurrentRoute(Transform route)
        {
            _currentRoute = route.GetComponent<Spawner>().GetCurrentRoute();
        }

        /// <summary>
        /// Resets the position and rotation of the item
        /// </summary>
        public void ResetPositionAndRotationBeforeRespawn()
        {
            gameObject.SetActive(false);
            _transform.position = _startPosition.position;
            _rigidbody.velocity = Vector3.zero;
        
            //reset rotation
            _transform.eulerAngles = Vector3.zero;
            _transform.localRotation = quaternion.Euler(Vector3.zero);
            _transform.rotation = Quaternion.identity;
            _rigidbody.freezeRotation = true;
            _objectPool.ReturnItemToPool(gameObject);
        }
        
        /// <summary>
        /// Sets the movement speed of the item    
        ///@param newMoveSpeed altered speed input
        /// </summary>
        public void SetMoveSpeed(float newMoveSpeed)
        {
            moveSpeed.Set(newMoveSpeed);
        }
        
        /// <summary>
        /// Sets movement towards the bubble
        /// </summary>
        private void MoveItemTowardsBubble()
        {
            var position = _currentPoint.position;
            transform.position =
                Vector3.MoveTowards(transform.position, position, moveSpeed.Get() * Time.deltaTime);
        }
    }
}
