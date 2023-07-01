using UnityEngine;
using LiteralRepository;

public class LoginSceneInitializer : SceneInitializer
{
    protected override void Awake()
    {
        base.Awake();
        
        Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
    }

    protected override void OnGetResources()
    {
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Manager, "PhotonLoginManager"));
    }
    
    //private void OnSceneLoaded()
}
