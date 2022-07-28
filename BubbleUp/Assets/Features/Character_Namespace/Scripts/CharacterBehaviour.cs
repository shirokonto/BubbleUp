using DataStructures.Variables;
using UnityEngine;

namespace Features.Character_Namespace.Scripts
{
    /**
     * Takes care of the characters' animation such as
     * walking during gameplay and falling when the bubble
     * bursts
     */

    public class CharacterBehaviour : MonoBehaviour
    {
        [SerializeField] private BoolVariable bubbleIsPopped;
        [SerializeField] private Rigidbody rigidBody;
        
        private Animator Animator { get; set; }
        private static readonly int IsFalling = Animator.StringToHash("IsFalling");

        private void Awake()
        {
            bubbleIsPopped.Set(false);
            Animator = GetComponent<Animator>();
            rigidBody = GetComponent<Rigidbody>();
        }
        
        private void Update()
        {
            Animator.SetBool(IsFalling, false);
            if (bubbleIsPopped.Get())
            {
                Animator.SetBool(IsFalling, true);
                rigidBody.useGravity = true;
            } 
        }
    }
}