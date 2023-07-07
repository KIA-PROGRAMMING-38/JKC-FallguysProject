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
        Instantiate(ResourceManager.Load<GameObject>
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameLoading, "GameLoadingSceneManager"));
        Instantiate(ResourceManager.Load<GameObject>
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameLoading, PathLiteral.UI, "GameLoadingBackgroundImage"));
        Instantiate(ResourceManager.Load<GameObject>
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameLoading, PathLiteral.UI, "HorizontalRendererViewController"));
        Instantiate(ResourceManager.Load<GameObject>
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameLoading, PathLiteral.UI, "MapInformationViewController"));
        Instantiate(ResourceManager.Load<GameObject>
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameLoading, PathLiteral.UI, "WhiteScreenViewController"));
        Instantiate(ResourceManager.Load<GameObject>
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameLoading, PathLiteral.UI, "GameLoadingMainPanelViewController"));
        

        if (PhotonNetwork.IsMasterClient)
        {
            string filePath = ResourceManager.SetDataPath(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameLoading, "MapSelectionManager");
            PhotonNetwork.Instantiate(filePath, transform.position, transform.rotation);
        }
    }

    protected override void InitializeModel()
    {
        Model.GameLoadingSceneModel.SetStatusLoadingSceneUI(true);
    }
}
