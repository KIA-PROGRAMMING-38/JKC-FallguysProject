using LiteralRepository;
using Photon.Pun;
using UnityEngine;

public class StageSceneInitializer : SceneInitializer
{
    
    protected override void OnGetResources()
    {
        Vector3 mapVector = new Vector3(0, 0, 0); 
        Quaternion mapRotation = Quaternion.Euler(0f, 180f,0f);
        
        Instantiate
            (DataManager.GetGameObjectData
                (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, "PhotonStageSceneEventManager"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, "StageManager"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, "ExitButtonViewController"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, "GoalPanelViewController"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, "StageExitPanelViewController"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, "StageStartViewController"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, "EntryCounterViewController"));

        if (!PhotonNetwork.IsMasterClient)
            return;
        
        PhotonNetwork.Instantiate
        (DataManager.SetDataPath
            (PathLiteral.Prefabs, "Stage", "FruitChute", "MapFruitChute"), mapVector, mapRotation);
    }
}
