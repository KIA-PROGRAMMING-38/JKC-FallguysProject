using System;
using System.IO;
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
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.UI, PathLiteral.Stage, "ExitButtonViewController"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.UI, PathLiteral.Stage, "PurposePanelViewController"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.UI, PathLiteral.Stage, "StageExitPanelViewController"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.UI, PathLiteral.Stage, "StageStartViewController"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.UI, PathLiteral.Stage, "RemainingTimeViewController"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.UI, PathLiteral.Stage, "ResultInStageViewController"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.UI, PathLiteral.Stage, "RoundEndViewController"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.UI, PathLiteral.Stage, "ObservedPlayerNameViewController"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Object, PathLiteral.Stage, "PlayerObserverCamera"));
        _stageAudioManager = ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Object, PathLiteral.Stage, "StageAudioManager"));

        SetStageAudioComponent();

        if (PhotonNetwork.IsMasterClient)
        {
            AsyncPhotonNetworkInstantiate();
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

    private void AsyncPhotonNetworkInstantiate()
    {
        string filePathInstantiateManager = Path.Combine(PathLiteral.Prefabs, PathLiteral.Object, PathLiteral.Stage, "StageInstantiateManager");
        PhotonNetwork.Instantiate(filePathInstantiateManager, transform.position, transform.rotation);
        
        string filePathPhotonRoomManager = Path.Combine(PathLiteral.Prefabs, PathLiteral.Object, PathLiteral.Stage, "PhotonStageSceneRoomManager");
        PhotonNetwork.Instantiate(filePathPhotonRoomManager, transform.position, transform.rotation);
    }
}
