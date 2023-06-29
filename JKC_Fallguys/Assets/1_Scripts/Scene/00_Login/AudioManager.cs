using System;
using LiteralRepository;
using Model;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    private AudioClip _loginSound;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _loginSound = DataManager.GetAudioClip(PathLiteral.Sounds, PathLiteral.Music, PathLiteral.LoginSound);
    }

    private void Start()
    {
        LoginSceneModel.OnLoginSuccess -= LoginSoundPlay;
        LoginSceneModel.OnLoginSuccess += LoginSoundPlay;
    }

    private void LoginSoundPlay()
    {
        _audioSource.PlayOneShot(_loginSound);
    }

    private void OnDestroy()
    {
        LoginSceneModel.OnLoginSuccess -= LoginSoundPlay;
    }
}
