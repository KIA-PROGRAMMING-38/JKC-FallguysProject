using ResourceRegistry;
using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    // Intro와 LoopMusic을 담당할 Audio Sources
    private AudioSource[] _audioSources = new AudioSource[2];
    
    // 외부에서 사용할 AudioSource
    public AudioSource[] MusicAudioSource => _audioSources;

    protected override void Awake()
    {
        base.Awake();

        for (int index = 0; index < _audioSources.Length; ++index)
        {
            _audioSources[index] = gameObject.AddComponent<AudioSource>();
            _audioSources[index].volume = 0.3f;
        }

        gameObject.AddComponent<AudioReverbZone>().reverbPreset = AudioReverbPreset.Room;
    }
}
