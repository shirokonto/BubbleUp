using DataStructures.Variables;
using UnityEngine;

namespace Features.Interactables_Namespace.Scripts
{
    public class ItemMover : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 0.5f;
        [SerializeField] private GameObject spawner;
        [SerializeField] private BoolVariable isSecondWave;
        private Transform _currentPoint;
        private Route _currentRoute;

        // Start is called before the first frame update
        void Start()
        {
            //Set initial position to first point of one of the three routes
            if (gameObject.transform.name.Contains("Clone"))
            {
                _currentRoute = spawner.GetComponent<SpawnController>().GetCurrentRoute();
        
                _currentPoint = _currentRoute.GetNextWayPoint(_currentPoint);
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

                var position = _currentPoint.position;
                transform.position =
                    Vector3.MoveTowards(transform.position, position, moveSpeed * Time.deltaTime);
            }
        }

        public void ScaleMoveSpeed(float percentage)
        {
            moveSpeed = moveSpeed * percentage;
        }
    }
}
