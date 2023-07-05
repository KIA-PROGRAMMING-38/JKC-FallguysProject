using LiteralRepository;
using Photon.Pun;

public class GameLoadingSceneInitializer : SceneInitializer
{
    protected override void Awake()
    {
        base.Awake();
        
        StageRepository.Instance.Initialize();
        
        if (AudioManager.Instance != null)
        {
            Destroy(AudioManager.Instance.gameObject);
        }
    }
    
    protected override void OnGetResources()
    {
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameLoading, "GameLoadingSceneManager"));
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameLoading, PathLiteral.UI, "GameLoadingBackgroundImage"));
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameLoading, PathLiteral.UI, "HorizontalRendererViewController"));
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameLoading, PathLiteral.UI, "MapInformationViewController"));
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameLoading, PathLiteral.UI, "WhiteScreenViewController"));
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameLoading, PathLiteral.UI, "GameLoadingMainPanelViewController"));
        

        if (PhotonNetwork.IsMasterClient)
        {
            string filePath = DataManager.SetDataPath(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameLoading, "MapSelectionManager");
            PhotonNetwork.Instantiate(filePath, transform.position, transform.rotation);
        }
    }

    protected override void InitializeModel()
    {
        Model.GameLoadingSceneModel.SetStatusLoadingSceneUI(true);
    }
}
