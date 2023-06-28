using LiteralRepository;
using Model;

public class MatchingStandbySceneInitializer : SceneInitializer
{
    private LineEffectPooler _lineEffectPooler;
    private RespawnZone _respawnZone;

    protected override void Start()
    {
        base.Start();
        
        _lineEffectPooler.OnInitialize(_respawnZone);
    }

    protected override void InitializeModel()
    {
        MatchingSceneModel.SetActiveEnterLobbyPanel(false);
        MatchingSceneModel.RoomAdmissionStatus(false);
        MatchingSceneModel.PossibleToExit(true);
    }

    protected override void OnGetResources()
    {
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.Object, "PhotonMatchingSceneEventManager"));
        
        _respawnZone = Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.Object, "RespawnZone"))
            .GetComponent<RespawnZone>();
        _lineEffectPooler = Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.Object, "LineEffectPooler"))
            .GetComponent<LineEffectPooler>();

        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.UI, "ReturnButtonViewController"));
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.UI, "ExitMatchingPanelViewController"));
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.Object, "ReleaseZone"));
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.UI, "MatchingStandbyBackgroundImage"));
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.UI, "RotationFaceIconViewController"));
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.UI, "CurrentParticipantsViewController"));
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.Object, "MatchingSceneFallGuy"));
    }
}
