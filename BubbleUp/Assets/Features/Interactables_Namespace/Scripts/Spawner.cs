using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructures.Variables;
using UnityEngine;
using Random = System.Random;
using RandomUnityEngine = UnityEngine.Random;

namespace Features.Interactables_Namespace.Scripts
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject spawnRoute;
        [SerializeField] private List<GameObject> incomingInfoItems;
        [SerializeField] private List<GameObject> incomingBlockerItems;
        [SerializeField] private List<GameObject> incomingPowerUpItems;
        [SerializeField] private FloatVariable infoItemAppearingPercentage;
        [SerializeField] private FloatVariable powerUpItemAppearingPercentage;
        [SerializeField] private BoolVariable isSecondWave;
        [SerializeField] private BoolVariable spawnRateBeginning;
        [SerializeField][Range(1f, 3f)] private float minRespawnTime;
        [SerializeField][Range(1f, 3f)] private float maxRespawnTime; 
        private ObjectPool _objectPool;
        private float _spawningItemDeterminer;
        private Random _random;
        private List<GameObject> _currentlySpawningItems;
        private Transform _startPosition;
        

        void Awake()
        {
            _objectPool = gameObject.GetComponent<ObjectPool>();
            _random = new Random();
        }

        // Start is called before the first frame update
        void Start()
        {
            spawnRateBeginning.Set(true);
            infoItemAppearingPercentage.Set(0.8f);
            powerUpItemAppearingPercentage.Set(0.9f);
            StartCoroutine(Spawn());
        }
        
        public Route GetCurrentRoute()
        {
            return spawnRoute.GetComponent<Route>();
        }

        private void Instantiate()
        {
            DetermineSpawningItem();
            GameObject newCritter = _objectPool.GetItem(ChooseRandomItemFromList(_currentlySpawningItems), spawnRoute.transform.GetChild(0).gameObject.transform);
        }

        private IEnumerator Spawn()
        {
            Instantiate();
            //if first wave = respawn time higher
            //if second wave = respawn time lower
            yield return new WaitForSeconds(RandomUnityEngine.Range(minRespawnTime, maxRespawnTime));
            StartCoroutine(Spawn());
        }

        /**
         * Only spawn infoItems in the first five seconds
         */
        private IEnumerator SpawnRateBeginning()
        {
            yield return new WaitForSeconds(5f);
            spawnRateBeginning.Set(false);
        }
        
        private GameObject ChooseRandomItemFromList(List<GameObject> items)
        {
            int index = _random.Next(items.Count);
            return items[index];
        }
        
        /**
         * Determines the amount of spawning objects in first and second wave
         */
        private void DetermineSpawningItem()
        {
            if (spawnRateBeginning)
            {
                _currentlySpawningItems = incomingInfoItems.ToList();
            }
            StartCoroutine(SpawnRateBeginning());
            
            if (isSecondWave.Get())
            {
                infoItemAppearingPercentage.Set(0.6f);
                powerUpItemAppearingPercentage.Set(0.8f);
            }
            _spawningItemDeterminer = RandomUnityEngine.Range(0f, 1f);
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
