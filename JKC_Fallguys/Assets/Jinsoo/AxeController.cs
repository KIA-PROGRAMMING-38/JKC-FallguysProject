using System.Threading;
using LiteralRepository;
using Photon.Pun;
using UnityEngine;

public class AxeController : MonoBehaviourPun
{
    private CancellationTokenSource _cancellationToken;
    
    private void Awake()
    {
        _cancellationToken = new CancellationTokenSource();
        
        InitializeObject();
    }

    private void InitializeObject()
    {
        ObjectTransforms axeData = DataManager.JsonLoader<ObjectTransforms>("JSON/AxeTransformData");

        for (int i = 0; i < axeData.positions.Length; ++i)
        {
            string filePath = DataManager.SetDataPath(PathLiteral.Prefabs, "Stage", PathLiteral.HoopLegend, "Axe");
            
            HoopLegendAxe axe = 
                PhotonNetwork.Instantiate(filePath, axeData.positions[i], Quaternion.Euler(axeData.rotations[i])).GetComponent<HoopLegendAxe>();
            axe.Initialize(this, _cancellationToken);
        }
    }

    private void OnDestroy()
    {
        _cancellationToken.Cancel();
    }
}
