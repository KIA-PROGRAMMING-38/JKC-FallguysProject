using System.IO;
using LiteralRepository;
using Model;
using Photon.Pun;
using ResourceRegistry;
using UnityEngine;

public class StageSceneInitializer : SceneInitializer
{
    protected override void InitializeData()
    {
        StageSceneModel.Clear();
        StageManager.Instance.Clear();
    }

    protected override void OnGetResources()
    {
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Object, PathLiteral.Stage, "PlayerObserverCamera"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Object, PathLiteral.Stage, "LandscapeViewer"));
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
    }

    protected override void OnGetResourcesOnMasterClient()
    {
        string filePathInstantiateManager = Path.Combine(PathLiteral.Prefabs, PathLiteral.Object, PathLiteral.Stage, "StageInstantiateManager");
        PhotonNetwork.Instantiate(filePathInstantiateManager, transform.position, transform.rotation);
        
        string filePathPhotonRoomManager = Path.Combine(PathLiteral.Prefabs, PathLiteral.Object, PathLiteral.Stage, "StageStateManager");
        PhotonNetwork.Instantiate(filePathPhotonRoomManager, transform.position, transform.rotation);
    }

    protected override void SetAudio()
    {
        AudioManager.Instance.Clear();
        
        if (!StageManager.Instance.StageDataManager.IsFinalRound())
        {
            int randomIndex = Random.Range(0, AudioRegistry.RoundMusic.Length);
            AudioManager.Instance.Play(SoundType.MusicLoop, AudioRegistry.RoundMusic[randomIndex], 0.3f);
        }

        else
        {
            AudioManager.Instance.Play(SoundType.MusicIntro, AudioRegistry.FinalRoundMusic[0], 0.3f);
            AudioManager.Instance.ScheduleLoopPlayback(AudioRegistry.FinalRoundMusic[1], 0.3f);
        }
    }
}
