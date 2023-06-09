using LiteralRepository;
using UnityEngine;

public class MatchingStandbySceneInitializer : SceneInitialize
{
    [Header("Object")]
    private LineEffectPooler _lineEffectPooler;
    private RespawnZone _respawnZone;
    
    [Header("Controller")]
    private ReturnButtonViewController _returnButtonViewController;
    private EnterLobbyFromMatchingViewController _enterLobbyFromMatchingViewController;

    [Header("Photon")] 
    private PhotonMatchingSceneRoomManager _photonMatchingSceneRoomManager;
    private PhotonMatchingSceneEventManager _photonMatchingSceneEventManager;
    
    protected override void Start()
    {
        base.Start();
        
        _lineEffectPooler.OnInitialize(_respawnZone);
        _returnButtonViewController.OnInitialize(_enterLobbyFromMatchingViewController);
        _photonMatchingSceneEventManager.OnInitialize(_photonMatchingSceneRoomManager);
    }
    
    protected override void OnGetResources()
    {
        _respawnZone = Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.Object, "RespawnZone"))
            .GetComponent<RespawnZone>();
        _lineEffectPooler = Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.Object, "LineEffectPooler"))
            .GetComponent<LineEffectPooler>();
        _returnButtonViewController = Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.UI, "ReturnButtonViewController"))
            .GetComponent<ReturnButtonViewController>();
        _enterLobbyFromMatchingViewController = Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.UI, "EnterLobbyFromMatchingViewController"))
            .GetComponent<EnterLobbyFromMatchingViewController>();
        _photonMatchingSceneRoomManager = Instantiate(DataManager.GetGameObjectData
                (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.Object, "PhotonMatchingSceneRoomManager"))
            .GetComponent<PhotonMatchingSceneRoomManager>();
        _photonMatchingSceneEventManager = Instantiate(DataManager.GetGameObjectData
                (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.Object, "PhotonMatchingSceneEventManager"))
            .GetComponent<PhotonMatchingSceneEventManager>();
        
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.Object, "ReleaseZone"));
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.UI, "MatchingStandbyBackgroundImage"));
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.UI, "RotationFaceIconViewController"));
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.UI, "CurrentParticipantsViewController"));
        
    }
}
