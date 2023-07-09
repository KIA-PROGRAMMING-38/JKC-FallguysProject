using System.IO;
using LiteralRepository;
using Model;
using Photon.Pun;
using ResourceRegistry;

public class GameResultSceneInitializer : SceneInitializer
{
    private bool _isVictory;
    protected override void InitializeModel()
    {
        _isVictory = StageDataManager.Instance.CachedPlayerIndicesForResults[0] == PhotonNetwork.LocalPlayer.ActorNumber;

        ResultSceneModel.CheckVictory(_isVictory);
    }

    protected override void OnGetResources()
    {
        SetAudio();
        
        GameResultSceneFallGuyController fallGuyController = 
            ResourceManager.Instantiate
                (Path.Combine(PathLiteral.Object, PathLiteral.GameResult, "GameResultSceneFallGuy"))
                .GetComponent<GameResultSceneFallGuyController>();
        
        fallGuyController.BodyRenderer.material.mainTexture = PlayerTextureRegistry.PlayerTextures[ResourceManager.PlayerTextureIndex.Value];
        
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Object, PathLiteral.GameResult, "GameResultPlatform"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Object, PathLiteral.GameResult, "GameResultScenePhotonController"));
        
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.UI, PathLiteral.GameResult, "GameResultBackgroundImage"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.UI, PathLiteral.GameResult, "ResultViewController"));
    }

    private void SetAudio()
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
}
