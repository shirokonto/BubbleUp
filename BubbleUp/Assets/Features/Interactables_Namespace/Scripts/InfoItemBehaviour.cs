using System;
using Leap.Unity.Interaction;
using UnityEngine;

namespace Features.Interactables_Namespace.Scripts
{
    public class InfoItemBehaviour : MonoBehaviour
    {
        public void PrintInfoItemColor()
        {
            Debug.Log("I'm a " + gameObject.name);
        }
    }
}