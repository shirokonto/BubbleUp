using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using Features.Interactables_Namespace.Scripts;
using UnityEngine;
using Leap;
using Leap.Unity.Interaction;

namespace Features.Hands_Namespace.Scripts
{
    public class HandBehaviour : MonoBehaviour
    {
        [SerializeField] private bool isLeftHand;
        private InteractionHand _interactionHand;
        
        //Get Collision By NameTag
        private List<string> _objectsByTagName;
        private List<string> _filteredTags;
        
        //Get Collision By GameObjects
        
        private List<GameObject> _filteredObjects;
        
        
        void Start(){
            _interactionHand = gameObject.GetComponent<InteractionHand>();
            _objectsByTagName = new List<string>();

        }
        void Update (){
            
            if (_interactionHand != null && _interactionHand.contactBones != null && _interactionHand.contactBones.Length != 0)
            {
                foreach (var gameObj in GetItemsByContactingInteractionBehaviours()){ //foreach (var elem in _filteredTags){  
                  
                    //TODO: Problem is that the tag "InfoObject" never leaves the filteredTags list --> so it will always go into the if statement
                    if (gameObj.CompareTag("InfoObject")) //if (elem.CompareTag("InfoObject"))
                    {
                        gameObj.GetComponent<InfoItemBehaviour>().PrintInfoItemColor();
                        gameObj.GetComponent<InfoItemBehaviour>().DisableItem();
                        String hand = _interactionHand.isLeft ? "left hand" : "right hand";
                        Debug.Log("Info Object was touched by " + hand);
                    } else if (gameObj.CompareTag("Virus")) //} else if (elem.CompareTag("Virus"))
                    {
                        //TODO: do this implementation in virus behaviour: set hand for two seconds still
                    }
                }
            }
        }
        
        private List<GameObject> GetItemsByContactingInteractionBehaviours()
        {
            List<GameObject> objects = new List<GameObject>();;
            var contactingInteractionBehaviour = _interactionHand.contactBones.GroupBy(bone => bone.GetContactingInteractionBehaviours());
            var enumerable = contactingInteractionBehaviour as IGrouping<List<IInteractionBehaviour>, ContactBone>[] ?? contactingInteractionBehaviour.ToArray();
                
            //filter for one tag
            foreach (var elem in enumerable)
            {
                var interactionBehaviours = elem.Key;
                foreach (var interactionBehaviour in interactionBehaviours)
                {
                    objects.Add(interactionBehaviour.gameObject);
                }
            }

            var filteredObjects = filterItemsBySameTagName(objects);
            return filteredObjects;

        }

        private List<GameObject> filterItemsBySameTagName(List<GameObject> objectsToBeFiltered)
        {
            //TODO: filter duplicate tags coming from the same interactionHand to one
            /*List<String> filteredTags = new List<string>();
            var objectsToBeFiltered = _objects.GroupBy(gameObj => gameObj.tag);
            foreach (var objectToBeFiltered in objectsToBeFiltered)
            {
                if (!filteredTags.Contains(objectToBeFiltered.Key))
                {
                    filteredTags.Add();
                }
            }
            List<GameObject> filteredObjects = new List<GameObject>();
            filteredObjects = _objects.Distinct().ToList();
            Debug.Log("filteredObjects: " + String.Join(", ",_filteredObjects));*/
            return objectsToBeFiltered;
        }
    }
}