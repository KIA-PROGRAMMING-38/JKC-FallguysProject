using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryState : StateMachineBehaviour
{
    public static event Action OnRecoveryState;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OnRecoveryState?.Invoke();
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
