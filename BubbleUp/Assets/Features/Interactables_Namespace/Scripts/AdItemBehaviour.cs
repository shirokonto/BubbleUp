using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdItemBehaviour : MonoBehaviour
{
    [SerializeField] private float activeDuration = 2;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.CompareTag("Bubble") && !collidedObject.GetComponent<BubbleBehaviour>().adBlockerEnabled)
        {
            StartCoroutine(SetTemporaryAd(collidedObject));
            Destroy(this.gameObject);
        }
    }
    
    private IEnumerator SetTemporaryAd(GameObject bubble)
    {
        bubble.GetComponent<SphereCollider>().enabled = false;
        
        yield return new WaitForSeconds(activeDuration);

        bubble.GetComponent<SphereCollider>().enabled = true;
    }
}
