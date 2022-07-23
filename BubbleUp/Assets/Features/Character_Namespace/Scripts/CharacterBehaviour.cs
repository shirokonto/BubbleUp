using DataStructures.Variables;
using UnityEngine;

namespace Features.Character_Namespace.Scripts
{
    /// <summary>
    /// Takes care of the characters' animation such as
    /// walking during gameplay and falling when the bubble
    /// bursts
    /// </summary>
    public class CharacterBehaviour : MonoBehaviour
    {
        [SerializeField] private BoolVariable bubbleIsPopped;
        [SerializeField] private Rigidbody rigidBody;
        
        //References getter and setter
        private Animator Animator { get; set; }
        private static readonly int IsFalling = Animator.StringToHash("IsFalling");

        private void Awake()
        {
            bubbleIsPopped.Set(false);
            Animator = GetComponent<Animator>();
            rigidBody = GetComponent<Rigidbody>();
        }
        
        // Update is called once per frame
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