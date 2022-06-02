using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    [SerializeField] private Waypoints waypoints; //Stores a refernce to the waypoint system this object will use
    [SerializeField] private float moveSpeed = 0.5f;

    private Transform _currentWaypoint;
    // Start is called before the first frame update
    void Start()
    {
        //Set initial position to first waypoint
        _currentWaypoint = waypoints.GetNextWayPoint(_currentWaypoint);
        transform.position = _currentWaypoint.position;
        
        //Set next waypoint target
        _currentWaypoint = waypoints.GetNextWayPoint(_currentWaypoint);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position =
            Vector3.MoveTowards(transform.position, _currentWaypoint.position, moveSpeed * Time.deltaTime);
    }
}
