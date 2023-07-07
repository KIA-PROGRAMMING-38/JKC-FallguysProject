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
        Instantiate(ResourceManager.Load<GameObject>
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, "LobbySceneFallGuy"));
        Instantiate(ResourceManager.Load<GameObject>
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI,"LobbyBackgroundImage"));
        Instantiate(ResourceManager.Load<GameObject>
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI,"PlayerNamePlateViewController"));
        Instantiate(ResourceManager.Load<GameObject>
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI,"TopButtonListViewController"));
        Instantiate(ResourceManager.Load<GameObject>
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI,"EnterConfigViewController"));
        Instantiate(ResourceManager.Load<GameObject>
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI,"EnterMatchingStandbyViewController"));
        
        Instantiate(ResourceManager.Load<GameObject>
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI,"HowToPlayViewController"));
        Instantiate(ResourceManager.Load<GameObject>
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI,"SettingsPanelViewController"));
        Instantiate(ResourceManager.Load<GameObject>
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI,"ConfigsViewController"));
        Instantiate(ResourceManager.Load<GameObject>
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI,"ConfigReturnButtonViewController"));
        
        Instantiate(ResourceManager.Load<GameObject>
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI, "Customization", "CostumeViewController"));
        Instantiate(ResourceManager.Load<GameObject>
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI, "Customization", "CustomizationViewController"));
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