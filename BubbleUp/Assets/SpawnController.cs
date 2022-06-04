using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnController : MonoBehaviour
{
    [Header("Incoming Objects")] 
    [SerializeField] private List<GameObject> incomingInfoObjects;
    [SerializeField] private List<GameObject> spawnRoutes;
    [SerializeField][Range(0.01f, 1f)] private float minRespawnTime;
    [SerializeField][Range(0.01f, 1f)] private float maxRespawnTime;
 

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }
    
    private void Instantiate()
    {
        GameObject currentRoute = spawnRoutes[Random.Range(0, spawnRoutes.Count)];
        GameObject infoItem = Instantiate(incomingInfoObjects[Random.Range(0, incomingInfoObjects.Count)], currentRoute.transform.GetChild(0).gameObject.transform);
        infoItem.GetComponent<ItemMover>().SetCurrentRoute(currentRoute.GetComponent<Route>());
        infoItem.transform.localPosition = Vector3.zero;
    }
    
    private IEnumerator Spawn()
    {
        Instantiate();
            
        yield return new WaitForSeconds(Random.Range(minRespawnTime, maxRespawnTime));

        StartCoroutine(Spawn());
    }
}
