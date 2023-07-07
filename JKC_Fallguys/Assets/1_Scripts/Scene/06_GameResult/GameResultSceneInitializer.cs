using LiteralRepository;
using Model;
using Photon.Pun;
using UnityEngine;

public class GameResultSceneInitializer : SceneInitializer
{
    protected override void InitializeModel()
    {
        bool isVictory = StageDataManager.Instance.CachedPlayerIndicesForResults[0] == PhotonNetwork.LocalPlayer.ActorNumber;

        ResultSceneModel.CheckVictory(isVictory);
    }

    protected override void OnGetResources()
    {
        GameResultSceneFallGuyController fallGuyController = 
            Instantiate
                (ResourceManager.Load<GameObject>(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameResult, "GameResultSceneFallGuy"))
                .GetComponent<GameResultSceneFallGuyController>();
        fallGuyController.BodyRenderer.material.mainTexture = PlayerTextureRegistry.PlayerTextures[ResourceManager.PlayerTextureIndex.Value];
        
        Instantiate
            (ResourceManager.Load<GameObject>(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameResult, "GameResultPlatform"));
        Instantiate
            (ResourceManager.Load<GameObject>(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameResult, PathLiteral.UI, "GameResultBackgroundImage"));
        Instantiate
            (ResourceManager.Load<GameObject>(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameResult, PathLiteral.UI, "ResultViewController"));
        Instantiate
            (ResourceManager.Load<GameObject>(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.GameResult, "GameResultScenePhotonController"));
    }
}
