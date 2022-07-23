using UnityEngine;

namespace Features.Interactables_Namespace.Scripts
{
    public class CritterReturn : MonoBehaviour
    {
        private ObjectPool _objectPool;

        private void Awake()
        {
            _objectPool = FindObjectOfType<ObjectPool>();
        }
        
        private void OnDisable()
        {
            if (_objectPool != null)
            {
                //_objectPool.ReturnItemToPool(this.gameObject);
            }
        }
    }
}
