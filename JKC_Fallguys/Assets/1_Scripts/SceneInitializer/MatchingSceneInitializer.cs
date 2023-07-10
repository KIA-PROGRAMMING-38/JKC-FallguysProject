using System.IO;
using LiteralRepository;
using Model;
using UnityEngine;

public class MatchingSceneInitializer : SceneInitializer
{
    private LineEffectPooler _lineEffectPooler;
    private RespawnZone _respawnZone;

    protected override void Start()
    {
        base.Start();
        
        _lineEffectPooler.OnInitialize(_respawnZone);
    }

    protected override void InitializeData()
    {
        MatchingSceneModel.SetActiveEnterLobbyPanel(false);
        MatchingSceneModel.RoomAdmissionStatus(false);
        MatchingSceneModel.PossibleToExit(true);
    }

    protected override void OnGetResources()
    {
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Object, PathLiteral.MatchingStandby, "PhotonMatchingSceneEventManager"));

        _respawnZone = ResourceManager.Instantiate
                (Path.Combine(PathLiteral.Object, PathLiteral.MatchingStandby, "RespawnZone"))
            .GetComponent<RespawnZone>();
        _lineEffectPooler = ResourceManager.Instantiate
                (Path.Combine(PathLiteral.Object, PathLiteral.MatchingStandby, "LineEffectPooler"))
            .GetComponent<LineEffectPooler>();

        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Object, PathLiteral.MatchingStandby, "ReleaseZone"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Object, PathLiteral.MatchingStandby, "MatchingSceneFallGuy"));
        
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.UI, PathLiteral.MatchingStandby, "MatchingBackgroundImage"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.UI, PathLiteral.MatchingStandby, "ExitMatchingPanelViewController"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.UI, PathLiteral.MatchingStandby, "ReturnButtonViewController"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.UI, PathLiteral.MatchingStandby, "CurrentParticipantsViewController"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.UI, PathLiteral.MatchingStandby, "RotationFaceIconViewController"));
    }
}
