using LiteralRepository;
using Photon.Pun;

public class RoundResultSceneInitializer : SceneInitializer
{
    protected override void OnGetResources()
    {
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.RoundResult, "RoundResultViewController"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.RoundResult, "Platform"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.RoundResult, "BackgroundImage"));
        Instantiate
            (DataManager.GetGameObjectData(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.RoundResult, "ResultRoundSetupManager"));
        
        if (PhotonNetwork.IsMasterClient)
        {
            string filePath = 
                DataManager.SetDataPath(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.RoundResult, "SceneChanger");

            PhotonNetwork.Instantiate(filePath, transform.position, transform.rotation);
        }
    }
}
