using Literal;
using UnityEngine;

public class JumpSubState : StateMachineBehaviour
{
    private PlayerInput _playerInput;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerInput = animator.GetComponentInParent<PlayerInput>();
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_playerInput.IsDive)
        {
            animator.SetBool(AnimLiteral.ISDIVING, true);
        }
    }
    
    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        // animator.SetBool(AnimLiteral.ISJUMPING, false);
    }
}
