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
        }
        
        // Update is called once per frame
        void Update()
        {
            Animator.SetBool(IsFalling, false);
            if (bubbleIsPopped.Get())
            {
                Animator.SetBool(IsFalling, true);
            } 
        }
    }
}