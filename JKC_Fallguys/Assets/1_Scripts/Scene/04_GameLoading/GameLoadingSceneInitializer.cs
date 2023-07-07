using System.IO;
using LiteralRepository;
using Photon.Pun;
using UnityEngine;

public class GameLoadingSceneInitializer : SceneInitializer
{
    protected override void Awake()
    {
        base.Awake();
        
        StageRepository.Instance.Initialize();
        
        if (AudioManager.Instance != null)
        {
            Destroy(AudioManager.Instance.gameObject);
        }
    }
    
    protected override void OnGetResources()
    {
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameLoading, "GameLoadingSceneManager"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameLoading, PathLiteral.UI, "GameLoadingBackgroundImage"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameLoading, PathLiteral.UI, "HorizontalRendererViewController"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameLoading, PathLiteral.UI, "MapInformationViewController"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameLoading, PathLiteral.UI, "WhiteScreenViewController"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameLoading, PathLiteral.UI, "GameLoadingMainPanelViewController"));
        

        if (PhotonNetwork.IsMasterClient)
        {
            string filePath = Path.Combine(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameLoading, "MapSelectionManager");
            PhotonNetwork.Instantiate(filePath, transform.position, transform.rotation);
        }
    }

    protected override void InitializeModel()
    {
        Model.GameLoadingSceneModel.SetStatusLoadingSceneUI(true);
    }
}
