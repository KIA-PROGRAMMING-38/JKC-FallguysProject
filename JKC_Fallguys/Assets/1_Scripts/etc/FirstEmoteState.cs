using UnityEngine;

public class FirstEmoteState : StateMachineBehaviour
{
    private AudioSource _audiosource;
    private GameResultFallGuyDataBinder _gameresultFallGuyDataBinder;
    override public void OnStateEnter( Animator animator, AnimatorStateInfo stateInfo, int layerIndex )
    {
        _audiosource = animator.GetComponent<AudioSource>();
        _gameresultFallGuyDataBinder = animator.GetComponent<GameResultFallGuyDataBinder>();
        _audiosource.clip = _gameresultFallGuyDataBinder.FallGuyAudioClips[0];
        _audiosource.Play();
    }
}
