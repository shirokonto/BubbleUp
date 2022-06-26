using System;
using System.Collections;
using System.Collections.Generic;
using Features.Bubble_Namespace.Scripts;
using UnityEngine;

public class AdBlockerBehaviour : MonoBehaviour
{
    [SerializeField] private float activeDuration = 3;
    
    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.CompareTag("Bubble") && !collidedObject.GetComponent<BubbleBehaviour>().adBlockerEnabled)
        {
            StartCoroutine(SetTemporaryAdBlocker(collidedObject));
            Destroy(this.gameObject);
        }
    }

    private IEnumerator SetTemporaryAdBlocker(GameObject bubble)
    {
        bubble.GetComponent<BubbleBehaviour>().adBlockerEnabled = true;
        
        yield return new WaitForSeconds(activeDuration);

        bubble.GetComponent<BubbleBehaviour>().adBlockerEnabled = false;
    }
}
