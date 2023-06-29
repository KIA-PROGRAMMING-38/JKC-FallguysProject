using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using LiteralRepository;
using Photon.Pun;
using UnityEngine;

public class AxeController : MonoBehaviourPun
{
    private CancellationTokenSource _cancellationToken;
    
    private void Awake()
    {
        _cancellationToken = new CancellationTokenSource();
        
        Test().Forget();
        
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

    private async UniTask Test()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(3f));
        
        InitializeObject();
    }

    private void OnDestroy()
    {
        _cancellationToken.Cancel();
    }
}
