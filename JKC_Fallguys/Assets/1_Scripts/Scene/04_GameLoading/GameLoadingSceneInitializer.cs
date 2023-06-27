using LiteralRepository;
using Photon.Pun;

public class GameLoadingSceneInitializer : SceneInitializer
{
    protected override void OnGetResources()
    {
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameLoading, "GameLoadingSceneManager"));
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameLoading, PathLiteral.UI, "GameLoadingBackgroundImage"));
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameLoading, PathLiteral.UI, "HorizontalRendererViewController"));
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameLoading, PathLiteral.UI, "GameLoadingMainPanelViewController"));
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameLoading, PathLiteral.UI, "WhiteScreenViewController"));
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameLoading, PathLiteral.UI, "MapInformationViewController"));
        
        string filePath = DataManager.SetDataPath(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameLoading, "MapSelectionManager");
        PhotonNetwork.Instantiate(filePath, transform.position, transform.rotation);
    }
}
