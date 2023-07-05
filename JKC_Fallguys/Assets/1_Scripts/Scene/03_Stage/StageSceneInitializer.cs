using System;
using Cysharp.Threading.Tasks;
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
        StageDataManager.Instance.PlayerContainer.Clear();
        
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
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, PathLiteral.UI, "RemainingTimeViewController"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, PathLiteral.UI, "ResultInStageViewController"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, PathLiteral.UI, "RoundEndViewController"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, PathLiteral.UI, "ObservedPlayerNameViewController"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, "PlayerObserverCamera"));
        _stageAudioManager = Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Manager, "StageAudioManager"));


        SetStageAudioComponent();
        

        if (PhotonNetwork.IsMasterClient)
        {
            AsyncPhotonNetworkInstantiate().Forget();
        }
    }

    private void SetStageAudioComponent()
    {
        if (!StageDataManager.Instance.IsFinalRound())
        {
            _stageAudioManager.AddComponent<RoundAudioScheduler>();
        }

        else
        {
            _stageAudioManager.AddComponent<FinalRoundAudioScheduler>();
        }
    }

    private async UniTaskVoid AsyncPhotonNetworkInstantiate()
    {
        string filePathInstantiateManager = DataManager.SetDataPath(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, "StageInstantiateManager");
        PhotonNetwork.Instantiate(filePathInstantiateManager, transform.position, transform.rotation);
        
        await UniTask.Delay(TimeSpan.FromSeconds(0.4f));
        
        string filePathPhotonRoomManager = DataManager.SetDataPath(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, "PhotonStageSceneRoomManager");
        PhotonNetwork.Instantiate(filePathPhotonRoomManager, transform.position, transform.rotation);
    }
}
