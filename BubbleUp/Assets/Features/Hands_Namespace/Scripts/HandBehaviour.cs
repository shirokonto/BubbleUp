using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Leap;
using Leap.Unity.Interaction;

namespace Features.Hands_Namespace.Scripts
{
    public class HandBehaviour : MonoBehaviour
    {
        [SerializeField] private bool isLeftHand;
        [SerializeField] private InteractionHand interactionHand;
        private Controller _controller;
        private Hand _hand;
        private List<Finger> _fingers;
        
        
        void Start(){
            _controller = new Controller();
            
        }
        void Update (){
            foreach (ContactBone contactBone in interactionHand.contactBones)
            {
                //contactBone.collider
            }

            Frame frame = _controller.Frame();
            bool handsDetected = frame.Hands.Any();
            if(handsDetected){ 
                //Debug.Log("frame.Hands: " + frame.Hands.Count);
                if (frame.Hands.Count == 1)
                {
                    //is it left or right hands?
                    _hand = frame.Hands[0];
                    String handName = _hand.IsRight ? "One hand: Right hand" : "One hand: Left hand";
                    //Debug.Log(handName);

                } else if (frame.Hands.Count == 2)
                {
                    //something else
                    _hand = frame.Hands[0];
                    _fingers = _hand.Fingers;
                    String handName = _hand.IsRight ? "Two hands: Right hand" : "Two hands: Left hand";
                    //Debug.Log(handName);
                }
            }
            // Debug.Log("Frame is: " + frame);
        }

        private void OnCollisionEnter(Collision collision)
        {   
            if (collision.gameObject.CompareTag("InfoObject"))
            {
                String handName = _hand.IsRight ? "Touched Right hand" : "Touched Left hand";
                Debug.Log("handName");

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