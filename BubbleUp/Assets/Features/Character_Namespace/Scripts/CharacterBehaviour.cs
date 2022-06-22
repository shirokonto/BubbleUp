using System;
using System.Collections;
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

        //References getter and setter
        public Animator Animator { get; set; }
        public Rigidbody Rigidbody { get; set; }
        private static readonly int IsFalling = Animator.StringToHash("IsFalling");


        private void Awake()
        {
            bubbleIsPopped.Set(false);
            Animator = GetComponent<Animator>();
            Rigidbody = GetComponent<Rigidbody>();
        }
        
        // Update is called once per frame
        void Update()
        {
            Animator.SetBool(IsFalling, false);
            if (bubbleIsPopped.Get())
            {
                Animator.SetBool(IsFalling, true);
                StartCoroutine(SetDelayedFall());
            } 
        }
        private IEnumerator SetDelayedFall()
        {
            yield return new WaitForSeconds(0.5f);
            Rigidbody.useGravity = true;
        }
    }
    
}