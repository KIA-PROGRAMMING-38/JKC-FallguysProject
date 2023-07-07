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
        Instantiate(Resources.Load<GameObject>(Path.Combine
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.Object, "PhotonMatchingSceneEventManager")));
        
        _respawnZone = Instantiate(Resources.Load<GameObject>(Path.Combine
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.Object, "RespawnZone")))
            .GetComponent<RespawnZone>();
        _lineEffectPooler = Instantiate(Resources.Load<GameObject>(Path.Combine
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.Object, "LineEffectPooler")))
            .GetComponent<LineEffectPooler>();
        
        Instantiate(Resources.Load<GameObject>(Path.Combine
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.Object, "ReleaseZone")));
        Instantiate(Resources.Load<GameObject>(Path.Combine
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.Object, "MatchingSceneFallGuy")));
    }
}
