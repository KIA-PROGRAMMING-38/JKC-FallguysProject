using System;
using LiteralRepository;
using ResourceRegistry;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundType
{
    MusicIntro,
    MusicLoop,
    SFX
}

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    private AudioSource[] _audioSources;

    protected override void Awake()
    {
        base.Awake();
        
        Init();
    }

    private void Init()
    {
        string[] soundTypeNames = Enum.GetNames(typeof(SoundType));
        _audioSources = new AudioSource[soundTypeNames.Length];
        for (int i = 0; i < soundTypeNames.Length; ++i)
        {
            GameObject go = new GameObject(soundTypeNames[i]);
            go.transform.parent = transform;
            _audioSources[i] = go.AddComponent<AudioSource>();
        }

        AudioSource musicLoop = _audioSources[(int)SoundType.MusicLoop];
        musicLoop.loop = true;
    }
}
