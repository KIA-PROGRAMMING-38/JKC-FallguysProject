using System.IO;
using LiteralRepository;
using Photon.Pun;
using UnityEngine;

public class RoundResultSceneInitializer : SceneInitializer
{
    protected override void OnGetResources()
    {
        Instantiate(Resources.Load<GameObject>
            (Path.Combine(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.RoundResult, "BackgroundImage")));
        Instantiate(Resources.Load<GameObject>
            (Path.Combine(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.RoundResult, "Platform")));
        Instantiate(Resources.Load<GameObject>
            (Path.Combine(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.RoundResult, "RoundResultViewController")));
        Instantiate(Resources.Load<GameObject>
            (Path.Combine(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.RoundResult, "ResultRoundSetupManager")));
        
        if (PhotonNetwork.IsMasterClient)
        {
            string filePath = 
                Path.Combine(PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.RoundResult, "SceneChanger");

            PhotonNetwork.Instantiate(filePath, transform.position, transform.rotation);
        }
    }
}
