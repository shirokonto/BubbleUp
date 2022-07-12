using System.Collections;
using System.Collections.Generic;
using DataStructures.Variables;
using Features.UI_Namespace.Script;
using UnityEngine;

namespace Features.Interactables_Namespace.Scripts
{
    public class SlowMotionBehaviour : MonoBehaviour
    {
        [SerializeField] private FloatVariable currentMoveSpeed;
        [SerializeField] private BoolVariable isSecondWave;
        private List<Transform> _allItemsOnSameRoute;
        private float _defaultMoveSpeed;

        private void Start()
        {
            _allItemsOnSameRoute = new List<Transform>();
            _defaultMoveSpeed = 0.4f;
            currentMoveSpeed.Set(_defaultMoveSpeed);
        }

        private void Update()
        {
            if (isSecondWave.Get())
            {
                _defaultMoveSpeed = 0.7f;
                currentMoveSpeed.Set(_defaultMoveSpeed);
                isSecondWave.Set(false); //if not set false it will override the slowMo functionality
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Bubble"))
            {
                //Trigger slowMo only if the current speed is as the default value
                if (currentMoveSpeed.Get() == _defaultMoveSpeed)
                {
                    Debug.Log("defaultMoveSpeed: " + _defaultMoveSpeed + " currentMoveSpeed: " + currentMoveSpeed.Get());
                    Transform path = transform.parent.gameObject.transform;
                    for (int i = 0; i < path.childCount; i++)
                    {
                        _allItemsOnSameRoute.Add(path.GetChild(i));
                    }
                    StartCoroutine(SlowDownFlowTemporary(_allItemsOnSameRoute));
                    SelectedItem.timer = true;
                }
                else
                {
                    GetComponent<ItemMover>().ResetPositionAndRotationBeforeRespawn();
                    SelectedItem.timer = false;
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
            SelectedItem.timer = false;
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
