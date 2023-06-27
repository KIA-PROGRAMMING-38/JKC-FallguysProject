using LiteralRepository;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonStageSceneEventManager : MonoBehaviourPunCallbacks
{
    private PhotonStageSceneRoomManager _roomManager;
    private GameObject _playerPrefab;
    
    private void Awake()
    {
        OnInstantiatePhotonRoomManager();
    }

    private void OnInstantiatePhotonRoomManager()
    {
        string filePath = DataManager.SetDataPath
            (PathLiteral.Prefabs, PathLiteral.Scene, PathLiteral.Stage, "PhotonStageSceneRoomManager");

        PhotonStageSceneRoomManager roomManager =
            PhotonNetwork.Instantiate(filePath, transform.position, transform.rotation)
            .GetComponent<PhotonStageSceneRoomManager>();

        _roomManager = roomManager;
    }

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
