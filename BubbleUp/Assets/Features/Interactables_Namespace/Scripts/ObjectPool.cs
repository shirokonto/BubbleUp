using System.Collections.Generic;
using UnityEngine;

namespace Features.Interactables_Namespace.Scripts
{
    public class ObjectPool : MonoBehaviour
    {
        //use of queue to store reference to game object
        private Dictionary<string, Queue<GameObject>> _itemPool;

        // Start is called before the first frame update
        void Start()
        {
            _itemPool = new Dictionary<string, Queue<GameObject>>();
        }

        public GameObject GetItem(GameObject item){
            if (_itemPool.TryGetValue(gameObject.name, out Queue<GameObject> itemList))
            {
                if (itemList.Count == 0)
                {
                    return CreateNewItem(item);
                }
                else
                {
                    GameObject pooledItem = itemList.Dequeue();
                    pooledItem.SetActive(true);
                    return pooledItem;
                }
            }
            else
            {
                return CreateNewItem(item);
            }
        }

        private GameObject CreateNewItem(GameObject item)
        {
            GameObject newItem = Instantiate(item);
            newItem.name = item.name;
            return newItem;
        }
        
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