using LiteralRepository;
using UnityEngine;

public class MovementState : StateMachineBehaviour
{
    private PlayerInputController _playerInputController;
    private PlayerPhysicsController _playerPhysicsController;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerInputController = animator.GetComponentInParent<PlayerInputController>();
        _playerPhysicsController = animator.GetComponent<PlayerPhysicsController>();
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if (_playerInputController.IsNothingUnderfoot)
        {
            animator.SetBool(AnimLiteral.IsFall, true);
            return;
        }
        
        _playerPhysicsController.Move();

        if (_playerInputController.IsJump)
        {
            animator.SetBool(AnimLiteral.IsJumping, true);
        }

        if (_playerInputController.IsAttemptingGrab)
        {
            animator.SetBool(AnimLiteral.IsGrab, true);
        }

        if (_playerInputController.IsDive)
        {
            animator.SetBool(AnimLiteral.IsDiving, true);
        }
    }
}
