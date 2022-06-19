using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionBehaviour : MonoBehaviour
{
    private List<Transform> _allItemsOnSameRoute;

    private void Start()
    {
        _allItemsOnSameRoute = new List<Transform>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bubble"))
        {
            //Change speed in itemMover moveSpeed only for siblings on same route
            
            Transform path = transform.parent.gameObject.transform;
            for (int i = 0; i < path.childCount; i++)
            {
                _allItemsOnSameRoute.Add(path.GetChild(i));
            }
            StartCoroutine(SlowDownFlowTemporary(_allItemsOnSameRoute));
            gameObject.SetActive(false);
        }
       
    }
    
    private IEnumerator SlowDownFlowTemporary(List<Transform> itemsOnRoute)
    {
        for (int i = 0; i < itemsOnRoute.Count; i++)
        {
            itemsOnRoute[i].GetComponent<ItemMover>().SetMoveSpeed(0.25f);
        }

        yield return new WaitForSeconds(5f);

        for (int i = 0; i < itemsOnRoute.Count; i++)
        {
            itemsOnRoute[i].GetComponent<ItemMover>().SetMoveSpeed(0.5f);
        }
    }
}
