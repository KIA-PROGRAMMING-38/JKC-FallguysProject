using Literal;
using UnityEngine;

public class MovementState : StateMachineBehaviour
{
    private PlayerInput _playerInput;
    private PlayerPhysicsController _playerPhysicsController;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerInput = animator.GetComponentInParent<PlayerInput>();
        _playerPhysicsController = animator.GetComponent<PlayerPhysicsController>();
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerPhysicsController.Move();

        // Jump키를 눌렀을때
        if (_playerInput.IsJump)
        {
            //Jump State로 전이
            animator.SetBool(AnimLiteral.ISJUMPING, true);
        }

        // Grab키를 눌렀을때
        if (_playerInput.IsAttemptingGrab)
        {
            animator.SetBool(AnimLiteral.ISGRAB, true);
        }

        if (_playerInput.IsDive)
        {
            animator.SetBool(AnimLiteral.ISDIVING, true);
        }
    }
}
