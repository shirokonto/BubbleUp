using System.Collections.Generic;
using UnityEngine;

namespace Features.Interactables_Namespace.Scripts
{
    
    [System.Serializable]
    public class ObjectPoolItem {
        public GameObject itemToPool; //currently only takes one!
        public int amountToPool; //number of items spawning
        public bool shouldExpand;
    }
    
    //Object Pooling Tutorial: https://www.raywenderlich.com/847-object-pooling-in-unity
    public class ObjectPooler : MonoBehaviour
    {
        public static ObjectPooler SharedInstance;
        public List<GameObject> pooledItems; //here list of all three item groups
        public List<ObjectPoolItem> itemsToPool;
        private Transform _firstRoute;
        private SpawnController _spawnController;

        
        private void Awake()
        {
            SharedInstance = this;
            _spawnController = GetComponent<SpawnController>();
        }

        // Start is called before the first frame update
        void Start()
        {
            pooledItems = new List<GameObject>();
            foreach (ObjectPoolItem item in itemsToPool) {
                for (int i = 0; i < item.amountToPool; i++) {
                    _firstRoute = _spawnController.GetFirstRoute();
                    GameObject obj = (GameObject)Instantiate(item.itemToPool, _firstRoute);
                    obj.SetActive(false);
                    pooledItems.Add(obj);
                }
            }
        }

        /**
         * Provides information about the current pooled Item
         * @param random chosen tag and route
         * @returns GameObject item
         */
       public GameObject GetPooledObject(string tag, Transform route) {
            for (int i = 0; i < pooledItems.Count; i++) {
                if (!pooledItems[i].activeInHierarchy && pooledItems[i].CompareTag(tag)) {
                    return pooledItems[i];
                }
            }
            foreach (ObjectPoolItem item in itemsToPool) {
                if (item.itemToPool.CompareTag(tag)) {
                    if (item.shouldExpand) {
                        GameObject obj = (GameObject)Instantiate(item.itemToPool, route);
                        obj.SetActive(false);
                        pooledItems.Add(obj);
                        return obj;
                    }
                }
            }
            return null;
        }
    }
}
