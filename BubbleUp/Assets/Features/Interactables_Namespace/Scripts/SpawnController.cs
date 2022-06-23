using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructures.Variables;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Features.Interactables_Namespace.Scripts
{
    public class SpawnController : MonoBehaviour
    {
        [Header("Incoming Items")] 
        [SerializeField] private List<GameObject> incomingInfoItems;
        [SerializeField] private List<GameObject> incomingBlockerItems;
        [SerializeField] private List<GameObject> incomingPowerUpItems;
        [SerializeField] private FloatVariable infoItemAppearingPercentage;
        [SerializeField] private FloatVariable powerUpItemAppearingPercentage;
        [SerializeField] private BoolVariable isSecondWave;
        [SerializeField] private List<GameObject> spawnRoutes;
        [SerializeField] private GameObject origSpawnRoute;
        [SerializeField][Range(0.01f, 1f)] private float minRespawnTime;
        [SerializeField][Range(0.01f, 1f)] private float maxRespawnTime;
        
        private GameObject _currentRoute;
        private float _spawningItemDeterminer;
        private List<GameObject> _currentlySpawningItems;

        private void Awake()
        {
            //TODO fix this so that the infoitems are not being spawnt randomly somewhere
            infoItemAppearingPercentage.Set(0.8f);
            powerUpItemAppearingPercentage.Set(0.9f);
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

            DetermineSpawningItem();
            GameObject spawningItem = Instantiate(_currentlySpawningItems[Random.Range(0, _currentlySpawningItems.Count)], _currentRoute.transform.GetChild(0).gameObject.transform);
            spawningItem.transform.localPosition = Vector3.zero;
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

        /**
         * Determines the amount of spawning objects in first and second wave
         */
        private void DetermineSpawningItem()
        {
            if (isSecondWave.Get())
            {
                infoItemAppearingPercentage.Set(0.6f);
                powerUpItemAppearingPercentage.Set(0.8f);
            }
            _spawningItemDeterminer = Random.Range(0f, 1f);
            if (_spawningItemDeterminer <= infoItemAppearingPercentage.Get())
            {
                _currentlySpawningItems = incomingInfoItems.ToList();
            }
            else if(_spawningItemDeterminer > infoItemAppearingPercentage.Get() &&  _spawningItemDeterminer <= powerUpItemAppearingPercentage.Get())
            {
                _currentlySpawningItems = incomingPowerUpItems.ToList();
            }
            else
            {
                _currentlySpawningItems = incomingBlockerItems.ToList();
            }
        }
    }
}
