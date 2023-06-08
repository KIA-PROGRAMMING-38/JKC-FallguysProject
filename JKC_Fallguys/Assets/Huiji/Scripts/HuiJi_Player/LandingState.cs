using System.Collections;
using System.Collections.Generic;
using Literal;
using UnityEngine;

public class LandingState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(AnimLiteral.ISRESPAWNING, false);
    }
}
