using LiteralRepository;
using Photon.Pun;
using UnityEngine;

public class RoundResultSceneInitializer : SceneInitializer
{
    protected override void OnGetResources()
    {
        Instantiate(ResourceManager.Load<GameObject>
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.RoundResult, "BackgroundImage"));
        Instantiate(ResourceManager.Load<GameObject>
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.RoundResult, "Platform"));
        Instantiate(ResourceManager.Load<GameObject>
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.RoundResult, "RoundResultViewController"));
        Instantiate(ResourceManager.Load<GameObject>
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.RoundResult, "ResultRoundSetupManager"));
        
        if (PhotonNetwork.IsMasterClient)
        {
            string filePath = 
                ResourceManager.SetDataPath(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.RoundResult, "SceneChanger");

            PhotonNetwork.Instantiate(filePath, transform.position, transform.rotation);
        }
    }
}
