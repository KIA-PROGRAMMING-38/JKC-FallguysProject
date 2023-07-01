using LiteralRepository;
using Photon.Pun;
using UnityEngine;

public class StageSceneInitializer : SceneInitializer
{
    private GameObject _stageAudioManager;
    protected override void InitializeModel()
    {
        StageDataManager.Instance.SetGameStatus(false);
        StageDataManager.Instance.SetRoundState(false);
        StageDataManager.Instance.SetPlayerState(StageDataManager.PlayerState.Default);
        StageDataManager.Instance.SetPlayerAlive(true);
    }

    protected override void OnGetResources()
    {
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, "PhotonStageSceneEventManager"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, "StageManager"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, PathLiteral.UI, "ExitButtonViewController"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, PathLiteral.UI, "PurposePanelViewController"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, PathLiteral.UI, "StageExitPanelViewController"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, PathLiteral.UI, "StageStartViewController"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, PathLiteral.UI, "EntryCounterViewController"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, PathLiteral.UI, "ResultInStageViewController"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, PathLiteral.UI, "RoundEndViewController"));
        _stageAudioManager = Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Manager, "StageAudioManager"));
        
        if (StageDataManager.Instance.IsFinalRound() == false)
        {
            _stageAudioManager.AddComponent<RoundAudioScheduler>();
        }

        else
        {
            _stageAudioManager.AddComponent<FinalRoundAudioScheduler>();
        }

        if (PhotonNetwork.IsMasterClient)
        {
            string filePath = DataManager.SetDataPath(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, "StageInstantiateManager");
            
            PhotonNetwork.Instantiate(filePath, transform.position, transform.rotation);    
        }
    }
}
