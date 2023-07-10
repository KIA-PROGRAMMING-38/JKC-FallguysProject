using System;
using LiteralRepository;
using ResourceRegistry;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    private Slider _volumeSlider;
    private Text _volumeValue;
    private string _audioMixerParameterName;

    private void Awake()
    {
        _volumeSlider = GetComponent<Slider>();
        _volumeValue = transform.Find( "VolumeValue" ).GetComponent<Text>();
        _audioMixerParameterName = transform.parent.name;
    }
    
    private void Start()
    {
        _volumeSlider.onValueChanged.AddListener(SetVolume);
        
        LoadConfigsValue();
    }

    private void LoadConfigsValue()
    {
        _volumeSlider.value = PlayerPrefs.GetFloat(_audioMixerParameterName, 1f);
        _volumeValue.text = PlayerPrefs.GetString(_audioMixerParameterName + "_Text", "100");
    }

    private void SetVolume(float volume)
    {
        AudioRegistry.GameAudioMixer.SetFloat( _audioMixerParameterName, Mathf.Log10( volume ) * 20 );
        
        DisplayVolumeValueText(volume);

        SaveConfigsValue();
    }

    private void DisplayVolumeValueText(float volume)
    {
        int displayValue = Mathf.RoundToInt(volume * 100);
        _volumeValue.text = displayValue.ToString();
    }

    private void SaveConfigsValue()
    {
        PlayerPrefs.SetFloat(_audioMixerParameterName, _volumeSlider.value);
        PlayerPrefs.SetString(_audioMixerParameterName + "_Text", _volumeValue.text);
    }
}
