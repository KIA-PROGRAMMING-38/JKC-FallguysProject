using ResourceRegistry;
using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    // Intro와 LoopMusic을 담당할 Audio Sources
    private AudioSource[] _audioSources = new AudioSource[2];
    
    // 외부에서 사용할 AudioSource
    public AudioSource[] MusicAudioSource => _audioSources;
    
    private AudioClip _loginSound;

    protected override void Awake()
    {
        base.Awake();

        for (int index = 0; index < _audioSources.Length; ++index)
        {
            _audioSources[index] = gameObject.AddComponent<AudioSource>();
            _audioSources[index].playOnAwake = false;

            _audioSources[index].clip = AudioRegistry.LobbyMusic[index];
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
