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
        Instantiate(Resources.Load<GameObject>(Path.Combine
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, "LobbySceneFallGuy")));
        Instantiate(Resources.Load<GameObject>(Path.Combine
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI,"LobbyBackgroundImage")));
        Instantiate(Resources.Load<GameObject>(Path.Combine
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI,"PlayerNamePlateViewController")));
        Instantiate(Resources.Load<GameObject>(Path.Combine
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI,"TopButtonListViewController")));
        Instantiate(Resources.Load<GameObject>(Path.Combine
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI,"EnterConfigViewController")));
        Instantiate(Resources.Load<GameObject>(Path.Combine
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI,"EnterMatchingStandbyViewController")));
        
        Instantiate(Resources.Load<GameObject>(Path.Combine
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI,"HowToPlayViewController")));
        Instantiate(Resources.Load<GameObject>(Path.Combine
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI,"SettingsPanelViewController")));
        Instantiate(Resources.Load<GameObject>(Path.Combine
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI,"ConfigsViewController")));
        Instantiate(Resources.Load<GameObject>(Path.Combine
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI,"ConfigReturnButtonViewController")));
        
        Instantiate(Resources.Load<GameObject>(Path.Combine
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI, "Customization", "CostumeViewController")));
        Instantiate(Resources.Load<GameObject>(Path.Combine
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, PathLiteral.UI, "Customization", "CustomizationViewController")));
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