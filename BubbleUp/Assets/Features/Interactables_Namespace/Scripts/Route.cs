using UnityEngine;

namespace Features.Interactables_Namespace.Scripts
{
    public class Route : MonoBehaviour
    {
        [Range(0f, 0.5f)]
        [SerializeField] private float wayPointSize = 1f;
    
        /**
     * Display the start and end points as blue spheres and route as a red line using gizmos
     */
        private void OnDrawGizmos()
        {
            foreach (Transform t in transform)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(t.position, wayPointSize); //take every transform for every waypoints
            }

            Gizmos.color = Color.red;
            for (int i = 0; i < transform.childCount - 1; i++)
            {
                Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i+1).position);
            }
        }

        /**
     * Returns the next way point of the path towards the bubble
     */
        public Transform GetNextWayPoint(Transform currentWayPoint)
        {
            if (currentWayPoint == null)
            {
                return transform.GetChild(0);
            }

            if (currentWayPoint.GetSiblingIndex() < transform.childCount - 1)
            {
                return transform.GetChild(currentWayPoint.GetSiblingIndex() + 1); //get next point
            }

            return transform.GetChild(0);
        }
    }
}
