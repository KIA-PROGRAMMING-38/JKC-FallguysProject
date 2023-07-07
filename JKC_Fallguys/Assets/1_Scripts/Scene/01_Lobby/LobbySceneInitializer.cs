using System.IO;
using LiteralRepository;
using Model;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbySceneInitializer : SceneInitializer
{
    protected override void Awake()
    {
        base.Awake();
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    protected override void InitializeModel()
    {
        LobbySceneModel.SetLobbyState(LobbySceneModel.LobbyState.Home);
    }

    protected override void OnGetResources()
    {
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, "LobbySceneFallGuy"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI,"LobbyBackgroundImage"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI,"PlayerNamePlateViewController"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI,"TopButtonListViewController"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI,"EnterConfigViewController"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI,"EnterMatchingStandbyViewController"));
        
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI,"HowToPlayViewController"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI,"SettingsPanelViewController"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI,"ConfigsViewController"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI,"ConfigReturnButtonViewController"));
        
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI, "Customization", "CostumeViewController"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI, "Customization", "CustomizationViewController"));
    }
    
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == SceneIndex.Lobby)
        {
            if (StageDataManager.Instance != null)
            {
                StageDataManager.Instance.DestorySelf();
            }
        }
    }
}