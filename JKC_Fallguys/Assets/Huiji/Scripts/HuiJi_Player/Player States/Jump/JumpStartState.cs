using System;
using UnityEngine;

public class JumpStartState : StateMachineBehaviour
{
    private PlayerPhysicsController _playerPhysicsController;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerPhysicsController = animator.GetComponent<PlayerPhysicsController>();
        
        animator.SetLayerWeight(1, 0);
        
        // 위로 점프한다.
        _playerPhysicsController.ActivateJumpAction();
    }
}
