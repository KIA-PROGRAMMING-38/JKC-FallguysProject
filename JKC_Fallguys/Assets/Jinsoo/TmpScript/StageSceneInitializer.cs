using LiteralRepository;

public class StageSceneInitializer : SceneInitialize
{
    protected override void OnGetResources()
    {
        Instantiate
            (DataManager.GetGameObjectData
                (PathLiteral.Prefabs, "FruitChute", "MapFruitChute"));
        Instantiate
            (DataManager.GetGameObjectData
                (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, "PhotonStageSceneEventManager"));
    }
}
