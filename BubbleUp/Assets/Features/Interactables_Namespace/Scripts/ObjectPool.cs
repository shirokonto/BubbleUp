using System.Collections.Generic;
using UnityEngine;

namespace Features.Interactables_Namespace.Scripts
{
    /**
     * Takes care of the creation, modification and item returning
     * of Object Pools for a given route.
     */
    public class ObjectPool : MonoBehaviour
    {
        //set of queue to store reference to game object
        private Dictionary<string, Queue<GameObject>> _itemPool;
        
        private void Awake()
        {
            _itemPool = new Dictionary<string, Queue<GameObject>>();
        }

        /**
         * Returns a clone of the passed item
         * @param item The item which should be cloned
         * @param route The route on which the cloned item should be spawned
         * @return the next item either from the pool or a new one
         */
        public GameObject GetItem(GameObject item, Transform route){
            if (_itemPool.TryGetValue(item.name + "(Clone)", out Queue<GameObject> itemList))
            {
                if (itemList.Count == 0)
                {
                    return CreateNewItem(item, route);
                }
                GameObject pooledItem = itemList.Dequeue();
                pooledItem.SetActive(true);
                return pooledItem;
            }
            return CreateNewItem(item, route);
        }

        /**
         * Returns a new clone of the passed item on the specific route
         * @param item The original item which should be cloned
         * @param route The route on which the cloned item should be spawned
         */
        private GameObject CreateNewItem(GameObject item, Transform route)
        {
            GameObject newItem = Instantiate(item, route);
            newItem.GetComponent<ItemMover>().SetCurrentRoute(route);
            return newItem;
        }
        
        /**
         * Returns the passed item to an existing pool.
         * If none exists a new pool will be created.
         * @param item The item which should be returned to the object pool
         */
        public void ReturnItemToPool(GameObject item)
        {
            if (_itemPool.TryGetValue(item.name, out Queue<GameObject> itemList))
            {
                itemList.Enqueue(item);
            }
            else
            {
                Queue<GameObject> newItemQueue = new Queue<GameObject>();
                newItemQueue.Enqueue(item);
                _itemPool.Add(item.name, newItemQueue);
            }
        }
    }
}