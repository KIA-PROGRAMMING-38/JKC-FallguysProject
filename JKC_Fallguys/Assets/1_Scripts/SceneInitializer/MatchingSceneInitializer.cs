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
            (Path.Combine(PathLiteral.Object, PathLiteral.Matching, "PhotonMatchingSceneEventManager"));

        _respawnZone = ResourceManager.Instantiate
                (Path.Combine(PathLiteral.Object, PathLiteral.Matching, "RespawnZone"))
            .GetComponent<RespawnZone>();
        _lineEffectPooler = ResourceManager.Instantiate
                (Path.Combine(PathLiteral.Object, PathLiteral.Matching, "LineEffectPooler"))
            .GetComponent<LineEffectPooler>();

        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Object, PathLiteral.Matching, "ReleaseZone"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Object, PathLiteral.Matching, "MatchingSceneFallGuy"));
        
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.UI, PathLiteral.Matching, "MatchingBackgroundImage"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.UI, PathLiteral.Matching, "ExitMatchingPanelViewController"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.UI, PathLiteral.Matching, "ReturnButtonViewController"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.UI, PathLiteral.Matching, "CurrentParticipantsViewController"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.UI, PathLiteral.Matching, "RotationFaceIconViewController"));
    }
}
