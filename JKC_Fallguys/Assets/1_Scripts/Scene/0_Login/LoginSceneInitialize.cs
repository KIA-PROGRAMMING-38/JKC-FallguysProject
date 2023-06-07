using UnityEngine;
using LiteralRepository;

public class LoginSceneInitialize : SceneInitialize
{
    private void Awake()
    {
        Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
    }

    protected override void OnGetResources()
    {
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.UI, "Scene_Login", "LoginBackgroundViewController"));
        Instantiate(DataManager.GetGameObjectData
                (PathLiteral.Prefabs, PathLiteral.UI, "Scene_Login", "LoginPanelViewController"));
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Manager, "PhotonLoginManager"));
    }
}
