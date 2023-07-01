using LiteralRepository;
using Photon.Pun;

public class RoundResultSceneInitializer : SceneInitializer
{
    protected override void OnGetResources()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            string filePath = 
                DataManager.SetDataPath(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.RoundResult, "SceneChanger");

            PhotonNetwork.Instantiate(filePath, transform.position, transform.rotation);
        }
    }
}
