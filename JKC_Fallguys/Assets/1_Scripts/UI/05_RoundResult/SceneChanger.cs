using System;
using Cysharp.Threading.Tasks;
using LiteralRepository;
using Photon.Pun;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    private const int LOAD_SCENE_DELAY = 10;
    
    private void Start()
    {
        LoadGameLoadingScene().Forget(); 
    }

    private async UniTaskVoid LoadGameLoadingScene()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(LOAD_SCENE_DELAY));

        PhotonNetwork.LoadLevel(SceneIndex.GameLoading);
    } 
}
