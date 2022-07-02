using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Features.Interactables_Namespace.Scripts
{
    public class CritterReturn : MonoBehaviour
    {
        private ObjectPool _objectPool;
        // Start is called before the first frame update
        void Start()
        {
            _objectPool = FindObjectOfType<ObjectPool>();
        }

        private void OnDisable()
        {
            if (_objectPool != null)
            {
                _objectPool.ReturnItemToPool(this.gameObject);
            }
        }
    }
}
