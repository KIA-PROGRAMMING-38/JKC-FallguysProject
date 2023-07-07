using System.IO;
using LiteralRepository;
using Photon.Pun;

public class RoundResultSceneInitializer : SceneInitializer
{
    protected override void OnGetResources()
    {
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.RoundResult, "BackgroundImage"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.RoundResult, "Platform"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.RoundResult, "RoundResultViewController"));
        ResourceManager.Instantiate
            (Path.Combine(PathLiteral.RoundResult, "ResultRoundSetupManager"));
        
        if (PhotonNetwork.IsMasterClient)
        {
            string filePath = 
                Path.Combine(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.RoundResult, "SceneChanger");

            PhotonNetwork.Instantiate(filePath, transform.position, transform.rotation);
        }
    }
}
