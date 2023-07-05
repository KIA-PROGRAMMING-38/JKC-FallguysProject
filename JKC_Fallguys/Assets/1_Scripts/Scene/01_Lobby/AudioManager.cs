using LiteralRepository;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    // Intro와 LoopMusic을 담당할 Audio Sources
    private AudioSource[] _audioSources = new AudioSource[2];
    
    // 외부에서 사용할 AudioSource
    public AudioSource[] MusicAudioSource => _audioSources;

    private AudioMixer _audioMixer;

    protected override void Awake()
    {
        base.Awake();

        _audioMixer = Resources.Load<AudioMixer>( DataManager.SetDataPath( PathLiteral.Sounds, PathLiteral.AudioMixer ) );

        for (int index = 0; index < _audioSources.Length; ++index)
        {
            _audioSources[index] = gameObject.AddComponent<AudioSource>();
            _audioSources[index].volume = 0.3f;

            _audioSources[index].outputAudioMixerGroup = _audioMixer.FindMatchingGroups( "Music" )[0];
        }

        gameObject.AddComponent<AudioReverbZone>().reverbPreset = AudioReverbPreset.Room;
    }
}
