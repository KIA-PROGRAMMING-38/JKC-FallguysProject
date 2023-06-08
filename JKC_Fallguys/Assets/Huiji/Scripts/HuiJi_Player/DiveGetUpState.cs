using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveGetUpState : StateMachineBehaviour
{
    public static event Action OnDiveGetUp;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OnDiveGetUp?.Invoke();
    }
}
