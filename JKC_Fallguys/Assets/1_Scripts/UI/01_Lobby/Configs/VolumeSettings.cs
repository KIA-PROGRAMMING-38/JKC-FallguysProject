using LiteralRepository;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    private Slider _volumeSlider;
    private Text _volumeValue;
    private AudioMixer _audioMixer;
    private string _audioMixerParameterName;

    private void Awake()
    {
        _volumeSlider = GetComponent<Slider>();
        _volumeValue = transform.Find( "VolumeValue" ).GetComponent<Text>();
        _audioMixer = Resources.Load<AudioMixer>( DataManager.SetDataPath( PathLiteral.Sounds, PathLiteral.AudioMixer ) );
        _audioMixerParameterName = transform.parent.name;
    }

    private void Start()
    {
        _volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    private void SetVolume(float volume)
    {
        _audioMixer.SetFloat( _audioMixerParameterName, Mathf.Log10( volume ) * 20 );
    }

}
