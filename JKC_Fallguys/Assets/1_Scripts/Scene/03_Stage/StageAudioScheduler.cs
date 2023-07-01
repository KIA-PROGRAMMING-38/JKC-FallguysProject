using ResourceRegistry;
using UnityEngine;

public class StageAudioScheduler : AudioScheduler
{
    private AudioSource _introAudioSource;

    private AudioSource _loopAudioSource;
    
    // Final Round일때
    private double _finalRoundIntroStartTime;
    private double _roundLoopStartTime;

    private void Awake()
    {
        if (StageDataManager.Instance.IsFinalRound())
        {
            _introAudioSource = gameObject.AddComponent<AudioSource>();
            _loopAudioSource = gameObject.AddComponent<AudioSource>();
        }

        else
        {
            _loopAudioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public override void SetAudioClip()
    {
        if (StageDataManager.Instance.IsFinalRound())
        {
            _introAudioSource.clip = AudioRegistry.FinalRoundMusic[0];
            _loopAudioSource.clip = AudioRegistry.FinalRoundMusic[1];
        }

        else
        {
            int randomAudioClipIndex = Random.Range(0, AudioRegistry.RoundMusic.Length);
            _loopAudioSource.clip = AudioRegistry.RoundMusic[randomAudioClipIndex];
        }
    }

    public override void SetMusicScheduleTime()
    {
        // finalRound일때
        if (StageDataManager.Instance.IsFinalRound())
        {
            _finalRoundIntroStartTime = AudioSettings.dspTime;
            _roundLoopStartTime = _finalRoundIntroStartTime + AudioRegistry.FinalRoundMusic[0].length;    
        }

        // Round일때
        else
        {
            _roundLoopStartTime = AudioSettings.dspTime;
        }
    }

    public override void PlayAudio()
    {
        if (StageDataManager.Instance.IsFinalRound())
        {
            ScheduleAudioSource(_introAudioSource, _finalRoundIntroStartTime, false);
            ScheduleAudioSource(_loopAudioSource, _roundLoopStartTime, true);
        }

        else
        {
            ScheduleAudioSource(_loopAudioSource, _roundLoopStartTime, true);
        }
    }
}
