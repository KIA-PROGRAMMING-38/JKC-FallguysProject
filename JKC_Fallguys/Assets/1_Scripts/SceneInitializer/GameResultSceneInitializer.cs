using System.IO;
using LiteralRepository;
using Model;
using Photon.Pun;

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
}
