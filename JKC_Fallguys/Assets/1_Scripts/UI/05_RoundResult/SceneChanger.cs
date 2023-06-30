using System;
using Cysharp.Threading.Tasks;
using LiteralRepository;
using Photon.Pun;

public class SceneChanger : MonoBehaviourPun
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

    private void OnDestroy()
    {
        foreach (PhotonView photonView in PhotonNetwork.PhotonViewCollection)
        {
            if (photonView.IsMine)
            {
                PhotonNetwork.Destroy(photonView);
            }
        }
    }
}