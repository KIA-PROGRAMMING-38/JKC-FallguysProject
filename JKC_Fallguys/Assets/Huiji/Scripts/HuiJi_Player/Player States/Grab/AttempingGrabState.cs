using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttempingGrabState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Grab상태가 되면서 Upper Layer의 weight를 1로 준다.
        animator.SetLayerWeight(layerIndex, 1);       
    }
}
