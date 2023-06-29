using LiteralRepository;
using Photon.Pun;

public class StageSceneInitializer : SceneInitializer
{
    protected override void OnGetResources()
    {
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, "PhotonStageSceneEventManager"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, "StageManager"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, PathLiteral.UI, "ExitButtonViewController"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, PathLiteral.UI, "PurposePanelViewController"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, PathLiteral.UI, "StageExitPanelViewController"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, PathLiteral.UI, "StageStartViewController"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, PathLiteral.UI, "EntryCounterViewController"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, PathLiteral.UI, "ResultInStageViewController"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, PathLiteral.UI, "RoundEndViewController"));

        if (PhotonNetwork.IsMasterClient)
        {
            string filePath = DataManager.SetDataPath(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, "StageInstantiateManager");
            
            PhotonNetwork.Instantiate(filePath, transform.position, transform.rotation);    
        }
    }
}
