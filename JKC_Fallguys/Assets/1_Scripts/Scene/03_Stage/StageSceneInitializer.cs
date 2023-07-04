using LiteralRepository;
using Model;
using Photon.Pun;
using UnityEngine;

public class StageSceneInitializer : SceneInitializer
{
    private GameObject _stageAudioManager;
    protected override void InitializeModel()
    {
        StageSceneModel.InitializeCountDown();
        
        StageDataManager.Instance.SetGameStatus(false);
        StageDataManager.Instance.SetRoundState(false);
        StageDataManager.Instance.SetGameStart(false);
        StageDataManager.Instance.SetPlayerActive(PhotonNetwork.LocalPlayer.ActorNumber, true);
        
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        StageDataManager.Instance.SetPlayerState(actorNumber, StageDataManager.PlayerState.Default);
    }


    protected override void OnGetResources()
    {
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
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, PathLiteral.UI, "ObservedPlayerNameViewController"));
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
            string filePathInstantiateManager = DataManager.SetDataPath(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, "StageInstantiateManager");
            string filePathPhotonEventManager = DataManager.SetDataPath(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, "PhotonStageSceneEventManager");
            string filePathPhotonRoomManager = DataManager.SetDataPath(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, "PhotonStageSceneRoomManager");
            
            PhotonNetwork.Instantiate(filePathInstantiateManager, transform.position, transform.rotation);
            PhotonNetwork.Instantiate(filePathPhotonEventManager, transform.position, transform.rotation);
            PhotonNetwork.Instantiate(filePathPhotonRoomManager, transform.position, transform.rotation);
        }
    }
}
