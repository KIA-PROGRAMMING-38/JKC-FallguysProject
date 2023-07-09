using ResourceRegistry;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayClapSFX()
    {
        _audioSource.PlayOneShot(AudioRegistry.FallGuySFXOnRoundResult[0]);
    }

    public void PlayThumbsUpSFX()
    {
        _audioSource.PlayOneShot(AudioRegistry.FallGuySFXOnRoundResult[1]);
    }

    public void PlayHighFiceSFX()
    {
        _audioSource.PlayOneShot(AudioRegistry.FallGuySFXOnRoundResult[2]);
    }

    public void PlayMaxWaveSFX()
    {
        _audioSource.PlayOneShot(AudioRegistry.VictoryFallGuySFX[0]);
    }

    public void PlayDanceSFX()
    {
        _audioSource.PlayOneShot(AudioRegistry.VictoryFallGuySFX[2]);
    }
    
    public void PlayArmThrowSFX()
    {
        _audioSource.PlayOneShot(AudioRegistry.LoseFallGuySFX[0]);
    }

    public void PlayChestBumpSFX()
    {
        _audioSource.PlayOneShot(AudioRegistry.LoseFallGuySFX[1]);
    }
    
    public void PlaySlowClapSFX()
    {
        _audioSource.PlayOneShot(AudioRegistry.LoseFallGuySFX[2]);
    }
}
