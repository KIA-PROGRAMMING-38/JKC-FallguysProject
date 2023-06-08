using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiteralRepository;
using Unity.VisualScripting;

public class MatchingStandbySceneInitializer : SceneInitialize
{
    private LineEffectPooler _lineEffectPooler;
    private RespawnZone _respawnZone;
    
    protected override void Start()
    {
        base.Start();
        
        _lineEffectPooler.OnInitialize(_respawnZone);
    }
    
    protected override void OnGetResources()
    {
        _respawnZone = Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.Object, "RespawnZone"))
            .GetComponent<RespawnZone>();
        _lineEffectPooler = Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.Object, "LineEffectPooler"))
            .GetComponent<LineEffectPooler>();
        
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.Object, "ReleaseZone"));
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.UI, "MatchingStandbyBackgroundImage"));
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.UI, "ReturnButtonViewController"));
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.UI, "RotaitionFaceIconViewController"));
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.UI, "CurrentParticipantsViewController"));
        Instantiate(DataManager.GetGameObjectData
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.MatchingStandby, PathLiteral.UI, "EnterLobbyFromMatchingViewController"));
    }
}
