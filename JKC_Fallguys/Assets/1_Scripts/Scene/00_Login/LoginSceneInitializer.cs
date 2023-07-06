using UnityEngine;
using LiteralRepository;
using ResourceRegistry;

public class LoginSceneInitializer : SceneInitializer
{
    protected override void Awake()
    {
        base.Awake();

        float volume = PlayerPrefs.GetFloat("MusicVolume");
        AudioRegistry.GameAudioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);

        Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
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
