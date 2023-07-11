using System;
using System.IO;
using Cysharp.Threading.Tasks;
using LiteralRepository;
using Model;
using ResourceRegistry;

public class RoundResultSceneInitializer : SceneInitializer
{
    protected override void InitializeData()
    {
        RoundResultSceneModel.Initialize();
    }
    
    protected override void OnGetResources()
    {
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Object, PathLiteral.RoundResult, "Platform"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Object, PathLiteral.RoundResult, "ResultRoundSetupManager"));
        
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.UI, PathLiteral.RoundResult, "BackgroundImage"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.UI, PathLiteral.RoundResult, "RoundResultViewController"));
    }

    protected override void OnGetResourcesOnMasterClient()
    {
        AsyncLoadGameLoadingScene().Forget();
    }

    protected override void SetAudio()
    {
        AudioManager.Instance.Clear();
        AudioManager.Instance.Play(SoundType.MusicIntro, AudioRegistry.RoundResultMusic, 0.3f);
        AudioManager.Instance.Play(SoundType.SFX, AudioRegistry.IncreaseNumbersSFX, 0.5f);
    }

    private const int LOAD_SCENE_DELAY = 10;
    private async UniTaskVoid AsyncLoadGameLoadingScene()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(LOAD_SCENE_DELAY));

        SceneChangeHelper.ChangeNetworkLevel(SceneIndex.GameLoading);
    }
}
