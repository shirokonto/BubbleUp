using System.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Leap; 
using Leap.Unity;
using Leap.Unity.Interaction;
using static Leap.Unity.Interaction.InteractionControllerSet;

namespace Features.Hands_Namespace.Scripts
{
    public class HandBehaviour : MonoBehaviour
    {

    Controller controller;
    Hand hand;
    List<Finger> fingers;
    void Start(){
        controller = new Controller();
    }
    void Update (){
        Frame frame = controller.Frame();

         if(frame.Hands.Count > 0){

            hand = frame.Hands[0];
            fingers = hand.Fingers;
            String handName = hand.IsLeft ? "Left hand" : "Right hand";
            Debug.Log(handName);
         }
       // Debug.Log("Frame is: " + frame);
    }

    private void OnCollisionEnter(Collision collision)
    {   
        if (collision.gameObject.CompareTag("InfoObject"))
        {
            Debug.Log("Touched left hand");

            /*
                Have to somehow replace the parameters for contactBone and InteractionObject like in the InteractionController.
            */
            //InteractionController.NotifyContactBoneCollisionEnter(contactBone, interactionObject);


            //collision.gameObject.GetComponent<Collider>().enabled = false;
            //StartCoroutine(DeactivateGameObject(10, collision));
    
        }
    }

        private IEnumerator DeactivateGameObject(float duration, Collision collision) {
            yield return new WaitForSeconds(duration);
            collision.gameObject.SetActive(false);
        }
    
}
}