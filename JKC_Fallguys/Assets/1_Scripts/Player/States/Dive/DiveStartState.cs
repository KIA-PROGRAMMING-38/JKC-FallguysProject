using System;
using UnityEngine;

public class DiveStartState : StateMachineBehaviour
{
    private PlayerPhysicsController _playerPhysicsController;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Upper Layer의 weight를 0으로 설정한다.
        animator.SetLayerWeight(1, 0);
        _playerPhysicsController = animator.GetComponent<PlayerPhysicsController>();
        _playerPhysicsController.ActivateDiveAction();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerPhysicsController.RestoreDiveHeightForce();
    }
}