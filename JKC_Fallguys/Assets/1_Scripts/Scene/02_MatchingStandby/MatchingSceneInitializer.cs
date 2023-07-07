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

    protected override void InitializeModel()
    {
        MatchingSceneModel.SetActiveEnterLobbyPanel(false);
        MatchingSceneModel.RoomAdmissionStatus(false);
        MatchingSceneModel.PossibleToExit(true);
    }

    protected override void OnGetResources()
    {
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.Object, "PhotonMatchingSceneEventManager"));
        
        _respawnZone = ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.Object, "RespawnZone"))
            .GetComponent<RespawnZone>();
        _lineEffectPooler = ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.Object, "LineEffectPooler"))
            .GetComponent<LineEffectPooler>();
        
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.Object, "ReleaseZone"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.Object, "MatchingSceneFallGuy"));
    }
}
