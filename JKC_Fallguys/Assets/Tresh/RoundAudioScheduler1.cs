using ResourceRegistry;
using UnityEngine;

public class RoundAudioScheduler : AudioScheduler
{
    private AudioSource _loopAudioSource;
    
    private double _loopStartTime;

    private void Awake()
    {
        _loopAudioSource = gameObject.AddComponent<AudioSource>();
        _loopAudioSource.volume = 0.3f;
    }

    public override void SetAudioClip()
    {
        int randomAudioClipIndex = Random.Range(0, AudioRegistry.RoundMusic.Length);
        _loopAudioSource.clip = AudioRegistry.RoundMusic[randomAudioClipIndex];
    }

    public override void SetMusicScheduleTime()
    {
        _loopStartTime = AudioSettings.dspTime;
    }

    public override void PlayAudio()
    {
        ScheduleAudioSource(_loopAudioSource, _loopStartTime, true);
    }
}
