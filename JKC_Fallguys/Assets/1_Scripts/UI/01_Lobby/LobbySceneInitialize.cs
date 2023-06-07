using LiteralRepository;

public class LobbySceneInitialize : SceneInitialize
{
    protected override void OnGetResources()
    {
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.UI, "01_Lobby", "LobbyBackgroundImage"));
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.UI, "01_Lobby", "PlayerNamePlateViewController"));
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.UI, "01_Lobby", "TopButtonListViewController"));
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.UI, "01_Lobby", "EnterConfigViewController"));
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.UI, "01_Lobby", "EnterMatchingStandbyViewController"));
    }
}
