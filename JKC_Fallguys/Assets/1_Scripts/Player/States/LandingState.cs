using System.Collections;
using System.Collections.Generic;
using LiteralRepository;
using UnityEngine;

public class LandingState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(AnimLiteral.IsRespawning, false);
    }
}
