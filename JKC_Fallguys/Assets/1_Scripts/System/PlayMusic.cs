using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    private double _introStartTime;
    private double _loopStartTime;
    void Start()
    {
        SetMusicScheduleTime();
        
        ScheduleAudioSouce(AudioManager.Instance.MusicAudioSource[0], _introStartTime, false);
        ScheduleAudioSouce(AudioManager.Instance.MusicAudioSource[1], _loopStartTime, true);
    }

    private void SetMusicScheduleTime()
    {
        _introStartTime = AudioSettings.dspTime;

        _loopStartTime = _introStartTime + AudioManager.Instance.MusicAudioSource[0].clip.length + 0.05f;

    }
    
    // 오디오 클립을 예약하여 재생하는 메서드입니다.
    void ScheduleAudioSouce(AudioSource audioSource, double startTime, bool loop)
    {
        audioSource.loop = loop;
        
        audioSource.PlayScheduled(startTime);
    }
}
