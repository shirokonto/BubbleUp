using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Features.Interactables_Namespace.Scripts
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private float timeToSpawn = 5f;
        [SerializeField] private List<GameObject> prefabs;
        private float _timeSinceSpawn;
        private ObjectPool _objectPool;
        private Random _random;

        // Start is called before the first frame update
        void Start()
        {
            _objectPool = FindObjectOfType<ObjectPool>();
            _random = new Random();
        }

        // Update is called once per frame
        void Update()
        {
            //instead of this use second objectPool script and instantiate script from before
            _timeSinceSpawn += Time.deltaTime;
            if (_timeSinceSpawn >= timeToSpawn)
            {
                GameObject newCritter = _objectPool.GetItem(ChooseRandomItemFromList());
                newCritter.transform.position = this.transform.position;
                _timeSinceSpawn = 0f;
            }
        }

        private GameObject ChooseRandomItemFromList()
        {
            int index = _random.Next(prefabs.Count);
            return prefabs[index];
        }
    }
}
