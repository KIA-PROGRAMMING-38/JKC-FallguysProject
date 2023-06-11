using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUpState : StateMachineBehaviour
{
    private PlayerPhysicsController _playerPhysicsController;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerPhysicsController = animator.GetComponent<PlayerPhysicsController>();
        _playerPhysicsController.ActivateGetUp();
    }
}
