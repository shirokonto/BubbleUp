using System;
using DataStructures.Variables;
using UnityEngine;
using Utilities.Event_Namespace;

namespace Features.Character_Namespace.Scripts
{
    public class CharacterBehaviour : MonoBehaviour
    {
        [Header("Events")]
        //[SerializeField] private GameEvent raiseEndScreen;
        [SerializeField] private BoolVariable bubbleIsPopped;

        [SerializeField] private Rigidbody rigidBody;
        //References getter and setter
        public Animator Animator { get; set; }
        
        private static readonly int IsFalling = Animator.StringToHash("IsFalling");

        private void Awake()
        {
            bubbleIsPopped.Set(false);
            Animator = GetComponent<Animator>();
            rigidBody = GetComponent<Rigidbody>();
            //Animator.SetBool(IsFalling, false);
        }
        
        

        // Update is called once per frame
        void Update()
        {
            Debug.Log("Animatorbool: " + Animator.GetBool(IsFalling));
            //Animator.SetBool(IsFalling, false);
            Debug.Log("bubble popped in character: " + bubbleIsPopped.Get());
            if (bubbleIsPopped)
            {
                Animator.SetBool(IsFalling, true);
                //rigidBody.useGravity = true;
                //raiseEndScreen.Raise();
            }
        }
        //if bubbleBurst is activated --> raiseEndScreen.Raise();
        
        /*private void OnTriggerEnter(Collider trigger)
        {
            if (trigger.gameObject.layer == 6)
            {
                raiseEndScreen.Raise();
            }
            /*if (trigger.TryGetComponent(typeof(SeekTriggerBehaviour), out Component seekTriggerBehaviour))
            {
                seekAnimatorState.SetNextState(seekTriggerBehaviour as SeekTriggerBehaviour);
                RequestState(seekAnimatorState);
            }
        }*/
    }
}
