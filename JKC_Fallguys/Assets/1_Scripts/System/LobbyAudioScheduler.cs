using ResourceRegistry;
using UnityEngine;

public class LobbyAudioScheduler : AudioScheduler
{
    private double _introStartTime;
    private double _loopStartTime;

    public override void SetAudioClip()
    {
        for (int i = 0; i < AudioManager.Instance.MusicAudioSource.Length; ++i)
        {
            AudioManager.Instance.MusicAudioSource[i].clip = AudioRegistry.LobbyMusic[i];    
        }
    }

    public override void SetMusicScheduleTime()
    {
        _introStartTime = AudioSettings.dspTime;

        _loopStartTime = _introStartTime + AudioManager.Instance.MusicAudioSource[0].clip.length + 0.05f;

    }

    public override void PlayAudio()
    {
        ScheduleAudioSource(AudioManager.Instance.MusicAudioSource[0], _introStartTime, false);
        ScheduleAudioSource(AudioManager.Instance.MusicAudioSource[1], _loopStartTime, true);
    }
}
