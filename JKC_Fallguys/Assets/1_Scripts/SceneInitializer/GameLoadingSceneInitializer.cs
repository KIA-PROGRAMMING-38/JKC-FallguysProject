using System.IO;
using LiteralRepository;
using Photon.Pun;
using ResourceRegistry;

public class GameLoadingSceneInitializer : SceneInitializer
{
    protected override void InitializeData()
    {
        Model.GameLoadingSceneModel.SetStatusLoadingSceneUI(true);
    }
    
    protected override void OnGetResources()
    {
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Object, PathLiteral.GameLoading, "GameLoadingSceneManager"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.UI, PathLiteral.GameLoading, "GameLoadingBackgroundImage"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.UI, PathLiteral.GameLoading, "HorizontalRendererViewController"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.UI, PathLiteral.GameLoading, "MapInformationViewController"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.UI, PathLiteral.GameLoading, "WhiteScreenViewController"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.UI, PathLiteral.GameLoading, "GameLoadingMainPanelViewController"));
    }
    
    protected override void OnGetResourcesOnMasterClient()
    {
        string filePath = Path.Combine(PathLiteral.Prefabs, PathLiteral.Object, PathLiteral.GameLoading, "MapSelectionManager");
        PhotonNetwork.Instantiate(filePath, transform.position, transform.rotation);
    }

    protected override void SetAudio()
    {
        AudioManager.Instance.Clear();
        AudioManager.Instance.Play(SoundType.MusicLoop, AudioRegistry.GameLoadingMusic, 0.3f);
    }
}
