using UnityEngine;
using LiteralRepository;
using ResourceRegistry;

public class LoginSceneInitializer : SceneInitializer
{
    protected override void Awake()
    {
        base.Awake();

        SetAudioMixerVolume();

        float currentMusicVolume;
        AudioRegistry.GameAudioMixer.GetFloat("MusicVolume", out currentMusicVolume);
        Debug.Log($"AudioMixer Music Volume : {currentMusicVolume}");

        Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
    }

    private void SetAudioMixerVolume()
    {
        float MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        AudioRegistry.GameAudioMixer.SetFloat("MasterVolume", Mathf.Log10(MasterVolume) * 20);

        float MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        AudioRegistry.GameAudioMixer.SetFloat("MusicVolume", Mathf.Log10(MusicVolume) * 20);

        float SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        AudioRegistry.GameAudioMixer.SetFloat("SFXVolume", Mathf.Log10(SFXVolume) * 20);
    }

    protected override void OnGetResources()
    {
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Manager, "PhotonLoginManager"));
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Login, "LoginBackgroundViewController"));
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Login, "LoginPanelViewController"));
    }
}
