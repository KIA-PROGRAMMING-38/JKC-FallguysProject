using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.MusicAudioSource.Play();
    }
}
