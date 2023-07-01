using LiteralRepository;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonStageSceneEventManager : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// 로비에 성공적으로 접속하였을 때 호출되는 콜백 메서드
    /// </summary>
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
    }

    public override void OnLeftRoom()
    {
        if (Model.MatchingSceneModel.IsEnterPhotonRoom.Value == false)
        {
            SceneManager.LoadScene(SceneIndex.Lobby);
        }
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}
