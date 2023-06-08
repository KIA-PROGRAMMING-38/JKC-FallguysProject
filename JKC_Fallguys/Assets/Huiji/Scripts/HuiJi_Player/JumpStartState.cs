using System;
using UnityEngine;

public class JumpStartState : StateMachineBehaviour
{
    private Rigidbody _playerRigidbody;

    public static event Action OnJump; 

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetLayerWeight(1, 0);
        OnJump?.Invoke();
    }
}
