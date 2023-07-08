using System;
using Cysharp.Threading.Tasks;
using LiteralRepository;
using Photon.Pun;
using UnityEngine;

public class GameLoadingSceneManager : MonoBehaviour
{
    [SerializeField]
    private float _deActiveDelayTime;
    
    private void Awake()
    {
        DeActiveRandomPickUIDelayTime().Forget();
    }

    private async UniTaskVoid DeActiveRandomPickUIDelayTime()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        
        Model.GameLoadingSceneModel.SetActiveWhitePanel(false);
        
        await UniTask.Delay(TimeSpan.FromSeconds(_deActiveDelayTime));

        Model.GameLoadingSceneModel.SetActiveWhitePanel(true);

        await UniTask.Delay(TimeSpan.FromSeconds(1f));

        Model.GameLoadingSceneModel.SetStatusLoadingSceneUI(false);

        await UniTask.Delay(TimeSpan.FromSeconds(0.8f));
        
        Model.GameLoadingSceneModel.SetActiveWhitePanel(false);
        
        await UniTask.Delay(TimeSpan.FromSeconds(_deActiveDelayTime));
        
        Model.GameLoadingSceneModel.SetActiveWhitePanel(true);
        
        await UniTask.Delay(TimeSpan.FromSeconds(2f));
        
        PhotonNetwork.LoadLevel(SceneIndex.Stage);
    }
}
