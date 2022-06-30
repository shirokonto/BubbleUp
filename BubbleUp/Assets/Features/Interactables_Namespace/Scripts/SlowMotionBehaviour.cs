using System.Collections;
using System.Collections.Generic;
using DataStructures.Variables;
using UnityEngine;

namespace Features.Interactables_Namespace.Scripts
{
    public class SlowMotionBehaviour : MonoBehaviour
    {
        [SerializeField] private FloatVariable currentMoveSpeed;
        [SerializeField] private BoolVariable isSecondWave;
        private List<Transform> _allItemsOnSameRoute;
        private float _defaultMoveSpeed;
        private Renderer _renderer;

        private void Start()
        {
            _allItemsOnSameRoute = new List<Transform>();
            _defaultMoveSpeed = 0.5f;
            currentMoveSpeed.Set(_defaultMoveSpeed);
            _renderer = GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            if (isSecondWave.Get())
            {
                _defaultMoveSpeed = 0.75f;
                currentMoveSpeed.Set(_defaultMoveSpeed);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Bubble"))
            {
                //Change speed in itemMover moveSpeed only for siblings on same route

                if (currentMoveSpeed.Get() == _defaultMoveSpeed)
                {
                    Transform path = transform.parent.gameObject.transform;
                    for (int i = 0; i < path.childCount; i++)
                    {
                        _allItemsOnSameRoute.Add(path.GetChild(i));
                    }
                    StartCoroutine(SlowDownFlowTemporary(_allItemsOnSameRoute));
                }
            }
        }
    
        private IEnumerator SlowDownFlowTemporary(List<Transform> itemsOnRoute)
        {
            SetSlowMoVisibility(false);
            for (int i = 0; i < itemsOnRoute.Count; i++)
            {
                //slow down speed by 20%
                itemsOnRoute[i].GetComponent<ItemMover>().SetMoveSpeed(_defaultMoveSpeed * 0.4f);
            }
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < itemsOnRoute.Count; i++)
            {
                //increase speed by 20%
                itemsOnRoute[i].GetComponent<ItemMover>().SetMoveSpeed(_defaultMoveSpeed);
            }
            SetSlowMoVisibility(true);
            GetComponent<ItemMover>().ResetPositionAndRotationBeforeRespawn();
        }

        private void SetSlowMoVisibility(bool isVisible)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(isVisible); // or false
            }
        }
    }
}
