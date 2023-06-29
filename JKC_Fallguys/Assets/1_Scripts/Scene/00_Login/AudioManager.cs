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
    }
}
