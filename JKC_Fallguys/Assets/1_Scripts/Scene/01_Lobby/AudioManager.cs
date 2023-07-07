using LiteralRepository;
using ResourceRegistry;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    // Intro와 LoopMusic을 담당할 Audio Sources
    private AudioSource[] _musicAudioSources = new AudioSource[2];
    private AudioSource _musicIntro => _musicAudioSources[0];
    private AudioSource _musicLoop => _musicAudioSources[1];
    
    // 외부에서 사용할 AudioSource
    public AudioSource[] MusicAudioSource => _musicAudioSources;


    protected override void Awake()
    {
        base.Awake();

        for (int index = 0; index < _musicAudioSources.Length; ++index)
        {
            _musicAudioSources[index] = gameObject.AddComponent<AudioSource>();
            _musicAudioSources[index].volume = 0.3f;

            _musicAudioSources[index].outputAudioMixerGroup = AudioRegistry.GameAudioMixer.FindMatchingGroups( "Music" )[0];
        }

        _musicIntro.loop = false;
        _musicLoop.loop = true;

        gameObject.AddComponent<AudioReverbZone>().reverbPreset = AudioReverbPreset.Room;
    }

    private void Start()
    {
        _musicIntro.clip = AudioRegistry.LobbyMusic[0];
        _musicLoop.clip = AudioRegistry.LobbyMusic[1];

        _musicIntro.Play();
        _musicLoop.PlayDelayed(AudioRegistry.LobbyMusic[0].length);
    }
}
