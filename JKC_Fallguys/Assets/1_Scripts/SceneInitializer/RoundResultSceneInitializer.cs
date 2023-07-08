using System.IO;
using LiteralRepository;
using Photon.Pun;

public class RoundResultSceneInitializer : SceneInitializer
{
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
        
        if (PhotonNetwork.IsMasterClient)
        {
            string filePath = 
                Path.Combine(PathLiteral.Prefabs, PathLiteral.UI, PathLiteral.RoundResult, "SceneChanger");

            PhotonNetwork.Instantiate(filePath, transform.position, transform.rotation);
        }
    }
}
