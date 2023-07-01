using ResourceRegistry;
using UnityEngine;

public class FinalRoundAudioScheduler : AudioScheduler
{
    private AudioSource _introAudioSource;

    private AudioSource _loopAudioSource;
    
    // Final Round일때
    private double _introStartTime;
    private double _loopStartTime;

    private void Awake()
    {
        _introAudioSource = gameObject.AddComponent<AudioSource>();
        _loopAudioSource = gameObject.AddComponent<AudioSource>();
    }

    public override void SetAudioClip()
    {
        _introAudioSource.clip = AudioRegistry.FinalRoundMusic[0];
        _loopAudioSource.clip = AudioRegistry.FinalRoundMusic[1];
    }

    public override void SetMusicScheduleTime()
    {
        _introStartTime = AudioSettings.dspTime;
        _loopStartTime = _introStartTime + AudioRegistry.FinalRoundMusic[0].length;
    }

    public override void PlayAudio()
    {
        ScheduleAudioSource(_introAudioSource, _introStartTime, false);
        ScheduleAudioSource(_loopAudioSource, _loopStartTime, true);
    }
}
