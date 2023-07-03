using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdEmoteState : StateMachineBehaviour
{
    private AudioSource _audiosource;
    private GameResultFallGuyDataBinder _gameresultFallGuyDataBinder;
    override public void OnStateEnter( Animator animator, AnimatorStateInfo stateInfo, int layerIndex )
    {
        _audiosource = animator.GetComponent<AudioSource>();
        _gameresultFallGuyDataBinder = animator.GetComponent<GameResultFallGuyDataBinder>();
        _audiosource.clip = _gameresultFallGuyDataBinder.FallGuyAudioClips[2];
        _audiosource.Play();
    }
}
