using System.Collections;
using System.Collections.Generic;
using DataStructures.Variables;
using Features.Interactables_Namespace.Scripts;
using UnityEngine;
using Utilities.Event_Namespace;

public class VirusBehaviour : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bubble"))
        {
            
        }
    }

}
