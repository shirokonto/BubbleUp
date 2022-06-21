using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private GameObject spawner;
    private Transform _currentPoint;
    private Route _currentRoute;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //Set initial position to first point of one of the three routes
        _currentRoute = spawner.GetComponent<SpawnController>().GetCurrentRoute();
        
        _currentPoint = _currentRoute.GetNextWayPoint(_currentPoint);
        transform.position = _currentPoint.position;
        
        //Set next waypoint target
        _currentPoint = _currentRoute.GetNextWayPoint(_currentPoint);
    }

    // Update is called once per frame
    void Update()
    {
        var position = _currentPoint.position;
        transform.position =
            Vector3.MoveTowards(transform.position, position, moveSpeed * Time.deltaTime);
    }

    public void SetMoveSpeed(float modifiedMoveSpeed)
    {
        moveSpeed = modifiedMoveSpeed;
    }
}
