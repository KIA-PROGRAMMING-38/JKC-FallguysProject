using UnityEngine;

public abstract class AudioScheduler : MonoBehaviour
{
    protected virtual void Start()
    {
        SetAudioClip();
        
        SetMusicScheduleTime();
        
        PlayAudio();
    }

    /// <summary>
    /// 오디오 클립을 할당합니다.
    /// </summary>
    public abstract void SetAudioClip();
    
    /// <summary>
    /// 음악이 시작되는 시간을 설정합니다. 
    /// </summary>
    public abstract void SetMusicScheduleTime();

    /// <summary>
    /// 스케줄을 짠 오디오 소스를 플레이 합니다. ScheduleAudioSource 메소드를 사용해서 스케줄을 짜주세요.
    /// </summary>
    public abstract void PlayAudio();
    
    protected void ScheduleAudioSource(AudioSource audioSource, double startTime, bool loop)
    {
        audioSource.loop = loop;
        
        audioSource.PlayScheduled(startTime);
    }
}
