using System;
using System.IO;
using Cysharp.Threading.Tasks;
using LiteralRepository;
using Model;
using Photon.Pun;
using ResourceRegistry;
using UnityEngine.SceneManagement;

public class GameResultSceneInitializer : SceneInitializer
{
    private bool _isVictory;
    protected override void InitializeData()
    {
        _isVictory = StageManager.Instance.PlayerContainer.CachedPlayerIndicesForResults[0] == PhotonNetwork.LocalPlayer.ActorNumber;

        ResultSceneModel.CheckVictory(_isVictory);
    }

    protected override void OnGetResources()
    {
        GameResultSceneFallGuyController fallGuyController = 
            ResourceManager.Instantiate
                (Path.Combine(PathLiteral.Object, PathLiteral.GameResult, "GameResultSceneFallGuy"))
                .GetComponent<GameResultSceneFallGuyController>();
        
        fallGuyController.BodyRenderer.material.mainTexture = PlayerTextureRegistry.PlayerTextures[LobbySceneModel.PlayerTextureIndex.Value];
        
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Object, PathLiteral.GameResult, "GameResultPlatform"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Object, PathLiteral.GameResult, "GameResultScenePhotonController"));
        
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.UI, PathLiteral.GameResult, "GameResultBackgroundImage"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.UI, PathLiteral.GameResult, "ResultViewController"));

        AsyncLoadLobbyScene().Forget();
    }

    protected override void SetAudio()
    {
        AudioManager.Instance.Clear();
        if (_isVictory)
        {
            AudioManager.Instance.Play(SoundType.MusicIntro, AudioRegistry.GameResultMusic[0], 0.3f);    
        }
        else
        {
            AudioManager.Instance.Play(SoundType.MusicIntro, AudioRegistry.GameResultMusic[1], 0.3f);
        }
    }

    private const int LOAD_SCENE_DELAY = 10;
    private async UniTaskVoid AsyncLoadLobbyScene()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(LOAD_SCENE_DELAY));

        SceneManager.LoadScene(SceneIndex.Lobby);
    }
}
