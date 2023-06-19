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

        if (!PhotonNetwork.IsMasterClient)
            return;
        
        PhotonNetwork.Instantiate
        (DataManager.SetDataPath
            (PathLiteral.Prefabs, "Stage", "FruitChute", "MapFruitChute"), mapVector, mapRotation);
    }
}
