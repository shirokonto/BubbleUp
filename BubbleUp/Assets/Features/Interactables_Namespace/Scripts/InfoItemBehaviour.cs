using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using UnityEngine;
public class InfoItemBehaviour : MonoBehaviour
    {
        
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Collision detected");
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("Touched left hand");
                
            }
        }
    }