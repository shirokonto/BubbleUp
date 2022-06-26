using System;
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
        [SerializeField][Range(0.01f, 1f)] private float minRespawnTime;
        [SerializeField][Range(0.01f, 1f)] private float maxRespawnTime;
        
        private GameObject _currentRoute;
        private float _spawningItemDeterminer;
        private List<GameObject> _currentlySpawningItems;
        
        private List<string> _tags;

        private void Awake()
        {
            //TODO fix this so that the infoitems are not being spawnt randomly somewhere
            infoItemAppearingPercentage.Set(0.8f);
            powerUpItemAppearingPercentage.Set(0.9f);
            //_currentRoute = origSpawnRoute;
            _tags = new List<string>();
            _tags.Add("InfoObject");
            _tags.Add("MinimizeBubble");
            _tags.Add("SlowMo");
        }

        // Start is called before the first frame update
        void OnEnable()
        {
            StartCoroutine(Spawn());
        }
        
        public Transform GetFirstRoute()
        {
            _currentRoute = spawnRoutes[Random.Range(0, spawnRoutes.Count)];
            return _currentRoute.transform.GetChild(0).gameObject.transform;
            
        }
    
        private void Instantiate()
        {
            //TODO: refactor "Tagging" to gameObject to use multiple infoItems
            _currentRoute = spawnRoutes[Random.Range(0, spawnRoutes.Count)];
            
            string tagging = _tags[Random.Range(0, _tags.Count)];
            DetermineSpawningItem();
            GameObject spawningItem = ObjectPooler.SharedInstance.GetPooledObject(tagging, _currentRoute.transform.GetChild(0).gameObject.transform); 
            if (spawningItem != null) {
                spawningItem.transform.localPosition = Vector3.zero;
                spawningItem.SetActive(true);
            }
            //GameObject spawningItem2 = Instantiate(_currentlySpawningItems[Random.Range(0, _currentlySpawningItems.Count)], _currentRoute.transform.GetChild(0).gameObject.transform);
            //spawningItem.transform.localPosition = Vector3.zero;
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
