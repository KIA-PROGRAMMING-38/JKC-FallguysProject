using Model;
using ResourceRegistry;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GameResultAudioScheduler : AudioScheduler
{
    private double _startAudioTime;

    private AudioSource _audioSource;
    private void Awake()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.volume = 0.1f;
    }
    public override void PlayAudio()
    {
        ScheduleAudioSource(_audioSource, _startAudioTime, false);
    }

    private AudioClip _victoryMusic;
    private AudioClip _loseMusic;
    public override void SetAudioClip()
    {
        _victoryMusic = AudioRegistry.GameResultMusic[0];
        _loseMusic= AudioRegistry.GameResultMusic[1];

        if ( ResultSceneModel.IsVictorious.Value )
        {
            _audioSource.clip = _victoryMusic;
        }

        else
        {
            _audioSource.clip = _loseMusic;
        }
    }

    public override void SetMusicScheduleTime()
    {
        _startAudioTime = AudioSettings.dspTime;
    }
}
