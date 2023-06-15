using System;
using Cysharp.Threading.Tasks;
using LiteralRepository;
using Photon.Pun;
using UnityEngine;

public class PhotonStageSceneRoomManager : MonoBehaviourPun
{
    [PunRPC]
    public void WinTheStage()
    {
        // 승리한 클라이언트의 정보에만 승리 기록해주기.
        // 나머지는 자동으로 패배가 기록되어, 다음 결과 창에서 승리 혹은 패배가 팝업됨.
        Debug.Log("승리!");
        Time.timeScale = 0f;
        
        photonView.RPC("EnterNextScene", RpcTarget.MasterClient);
    }

    private async UniTaskVoid StageEndProduction()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(2f), DelayType.UnscaledDeltaTime);

        Debug.Log("Hi");
        Debug.Log(SceneIndex.GameResult);
        PhotonNetwork.LoadLevel(SceneIndex.GameResult);
    }

    [PunRPC]
    public void EnterNextScene()
    {
        StageEndProduction().Forget();
    }
}
