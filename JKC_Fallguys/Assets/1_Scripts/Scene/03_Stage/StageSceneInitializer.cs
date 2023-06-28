using LiteralRepository;

public class StageSceneInitializer : SceneInitializer
{
    
    protected override void OnGetResources()
    {
        Instantiate
            (DataManager.GetGameObjectData
                (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, "PhotonStageSceneEventManager"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, "StageManager"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, "ExitButtonViewController"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, "PurposePanelViewController"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, "StageExitPanelViewController"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, "StageStartViewController"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, "EntryCounterViewController"));
    }
}
