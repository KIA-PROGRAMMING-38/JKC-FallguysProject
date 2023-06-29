using ResourceRegistry;
using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    // 외부에서 사용할 AudioSource
    public AudioSource MusicAudioSource { get; private set; }
    
    // Intro와 LoopMusic을 담당할 Audio Sources
    private AudioSource[] _audioSources = new AudioSource[2];
    
    private AudioClip _loginSound;

    protected override void Awake()
    {
        base.Awake();

        for (int index = 0; index < _audioSources.Length; ++index)
        {
            _audioSources[index] = gameObject.AddComponent<AudioSource>();
            
            // 클립 두개 저장해야함.
            //ScheduleAudioClip(_audioSources[index], );
        }
        
        // MusicAudioSource에 Play할 AudioSource 할당해야 함.
    }
    
    // 오디오 클립을 예약하여 재생하는 메서드
    void ScheduleAudioClip(AudioSource audioSource ,AudioClip clip, double startTime, bool loop)
    {
        audioSource.clip = clip;
        audioSource.loop = loop; // 루프 여부 설정

        // 오디오 클립을 예약하여 재생
        audioSource.PlayScheduled(startTime);
    }

    public void DestorySelf()
    {
        Destroy(gameObject);
    }
}
