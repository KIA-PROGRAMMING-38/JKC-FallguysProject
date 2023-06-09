using LiteralRepository;
using UniRx;
using UnityEngine;

public class MatchingStandbySceneInitializer : SceneInitialize
{
    private LineEffectPooler _lineEffectPooler;
    private RespawnZone _respawnZone;
    private ReturnButtonViewController _returnButtonViewController;
    private EnterLobbyFromMatchingViewController _enterLobbyFromMatchingViewController;
    
    protected override void Start()
    {
        base.Start();
        
        _lineEffectPooler.OnInitialize(_respawnZone);
        _returnButtonViewController.SetReference(_enterLobbyFromMatchingViewController);

        Observable.EveryUpdate()
            .ObserveEveryValueChanged(_ => Model.MatchingSceneModel.IsEnterLobbyFromMatching)
            .Subscribe(_ => Debug.Log(Model.MatchingSceneModel.IsEnterLobbyFromMatching));
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
