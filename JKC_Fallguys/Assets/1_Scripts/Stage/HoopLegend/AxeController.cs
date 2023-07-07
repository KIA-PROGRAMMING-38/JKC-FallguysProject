using System.IO;
using LiteralRepository;
using Photon.Pun;
using UnityEngine;

public class AxeController : MonoBehaviourPun
{
    private void Awake()
    {
        InitializeObject();
    }

    private void InitializeObject()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            ObjectTransforms axeData = ResourceManager.JsonLoader<ObjectTransforms>("JSON/AxeTransformData");

            for (int i = 0; i < axeData.positions.Length; ++i)
            {
                string filePath = Path.Combine(PathLiteral.Prefabs, "Stage", PathLiteral.HoopLegend, "Axe");
            
                PhotonNetwork.Instantiate
                    (filePath, axeData.positions[i], Quaternion.Euler(axeData.rotations[i]));
            }
        }
    }
}
