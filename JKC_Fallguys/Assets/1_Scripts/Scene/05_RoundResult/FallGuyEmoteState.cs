using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallGuyEmoteState : StateMachineBehaviour
{
    private AudioSource _audioSource;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _audioSource = animator.GetComponent<AudioSource>();
        _audioSource.Play();
    }
}
