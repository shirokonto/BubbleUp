using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class SpawnController : MonoBehaviour
{
    [Header("Incoming Objects")] 
    [SerializeField] private List<GameObject> incomingInfoObjects;
    [SerializeField] private List<GameObject> incomingBlockerObjects;
    [SerializeField] private List<GameObject> incomingPowerUpObjects;
    [SerializeField] private List<GameObject> spawnRoutes;
    [SerializeField] private GameObject origSpawnRoute;
    [SerializeField][Range(0.01f, 1f)] private float minRespawnTime;
    [SerializeField][Range(0.01f, 1f)] private float maxRespawnTime;
    private GameObject _currentRoute;

    private void Awake()
    {
        _currentRoute = origSpawnRoute;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }
    
    private void Instantiate()
    {
        _currentRoute = spawnRoutes[Random.Range(0, spawnRoutes.Count)];
        
        GameObject infoItem = Instantiate(incomingInfoObjects[Random.Range(0, incomingInfoObjects.Count)], _currentRoute.transform.GetChild(0).gameObject.transform);
        infoItem.transform.localPosition = Vector3.zero;
    }
    
    private IEnumerator Spawn()
    {
        Instantiate();
            
        yield return new WaitForSeconds(Random.Range(minRespawnTime, maxRespawnTime));

        StartCoroutine(Spawn());
    }

    public Route GetCurrentRoute()
    {
        return _currentRoute.GetComponent<Route>();
    }
}
