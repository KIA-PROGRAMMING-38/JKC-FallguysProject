using ResourceRegistry;
using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    public AudioSource MusicAudioSource { get; private set; }
    private AudioClip _loginSound;

    protected override void Awake()
    {
        base.Awake();
        
        gameObject.AddComponent<AudioSource>();
        MusicAudioSource = GetComponent<AudioSource>();

        MusicAudioSource.clip = AudioRegistry.LobbyMusic;
    }

    public void DestorySelf()
    {
        Destroy(gameObject);
    }
}
