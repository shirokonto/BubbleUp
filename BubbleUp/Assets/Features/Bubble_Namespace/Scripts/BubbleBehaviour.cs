using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBehaviour : MonoBehaviour
{
    [SerializeField] private string correctInfoType; //will be set before the game starts via character view
    private int _temporaryCount;

    private void Awake()
    {
        _temporaryCount = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("InfoObject"))
        {
            if (collision.gameObject.transform.name.Contains(correctInfoType))
            {
                _temporaryCount += 1;
                Debug.Log("( + ) TemporaryCount: " + _temporaryCount + " Nice!!! ");
            }
            else
            {
                _temporaryCount -= 1;
                Debug.Log("( - ) TemporaryCount: " + _temporaryCount + " :-( ");
            }
            collision.gameObject.SetActive(false);
        }
        
        //adblocker and co.
            
    }
}
