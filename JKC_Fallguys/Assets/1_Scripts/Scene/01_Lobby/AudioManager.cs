using System;
using JetBrains.Annotations;
using UnityEngine;

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

    public void Play(SoundType soundType, [NotNull] AudioClip audioClip, float volume)
    {
        AudioSource audioSource = _audioSources[(int)soundType];
        audioSource.volume = volume;
        
        switch(soundType)
        {
            case SoundType.MusicIntro:
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }

                audioSource.clip = audioClip;
                audioSource.Play();
                break;
            
            case SoundType.MusicLoop:
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }

                audioSource.clip = audioClip;
                audioSource.Play();
                break;
            
            case SoundType.SFX:
                audioSource.PlayOneShot(audioClip);
                break;
            
            default:
                Debug.LogError($"AudioManager : You are not implemented at {nameof(Play)}");
                break;
        }
    }

    public void ScheduleLoopPlayback(AudioClip audioClip, float volume)
    {
        AudioSource loopAudioSource = _audioSources[(int)SoundType.MusicLoop];
        loopAudioSource.volume = volume;

        AudioSource introAudioSource = _audioSources[(int)SoundType.MusicIntro];
        loopAudioSource.PlayDelayed(introAudioSource.clip.length);
    }
}
