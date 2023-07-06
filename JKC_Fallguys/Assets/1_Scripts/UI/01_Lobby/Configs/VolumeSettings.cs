using System;
using LiteralRepository;
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
        
        LoadConfigsValue();
        
        // Login Scene에서 작성해야 함.
        _audioMixer.SetFloat(_audioMixerParameterName, Mathf.Log10(_volumeSlider.value) * 20);
    }

    private void LoadConfigsValue()
    {
        _volumeSlider.value = PlayerPrefs.GetFloat(_audioMixerParameterName, 1f);
        _volumeValue.text = PlayerPrefs.GetString(_audioMixerParameterName + "_Text", "100");
    }

    private void SetVolume(float volume)
    {
        _audioMixer.SetFloat( _audioMixerParameterName, Mathf.Log10( volume ) * 20 );
        
        DisplayVolumeValueText(volume);
    }

    private void DisplayVolumeValueText(float volume)
    {
        int displayValue = Mathf.RoundToInt(volume * 100);
        _volumeValue.text = displayValue.ToString();
    }

    private void OnDisable()
    {
        SaveConfigsValue();
    }

    private void SaveConfigsValue()
    {
        PlayerPrefs.SetFloat(_audioMixerParameterName, _volumeSlider.value);
        PlayerPrefs.SetString(_audioMixerParameterName + "_Text", _volumeValue.text);
    }
}
