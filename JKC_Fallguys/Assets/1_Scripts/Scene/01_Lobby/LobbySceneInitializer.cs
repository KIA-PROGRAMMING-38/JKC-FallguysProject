using LiteralRepository;
using Model;
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
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Lobby, "LobbySceneFallGuy"));
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