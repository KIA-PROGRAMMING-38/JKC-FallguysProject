using ExitGames.Client.Photon;
using LiteralRepository;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonStageSceneEventManager : MonoBehaviourPunCallbacks
{
    private PhotonStageSceneRoomManager _roomManager;
    private GameObject _playerPrefab;
    
    private Vector3[] _spawnPoints =
    {
        new Vector3(0, 0, 0),
        new Vector3(-11, 0, -10), new Vector3(-5, 0, -10),
        new Vector3(5, 0, -10), new Vector3(11, 0, -10),
        new Vector3(-11, 0, -13), new Vector3(-5, 0, -13),
        new Vector3(-11, 0, -13), new Vector3(-5, 0, -13),
    };

    private void Awake()
    {
        SceneManager.sceneLoaded += OnInitializeLoadScene;
        
        SetPlayerSpawnPoints();
        OnInstantiatePhotonRoomManager();
    }

    private void Start()
    {
        PlayerReferenceManager playerReferenceManager = _playerPrefab.GetComponent<PlayerReferenceManager>();
        playerReferenceManager.OnInitialize(_roomManager);
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

    private void SetPlayerSpawnPoints()
    {
        string filePath = DataManager.SetDataPath(PathLiteral.Prefabs, "Player");
        Vector3 spawnPoint = _spawnPoints[PhotonNetwork.LocalPlayer.ActorNumber];
    
        _playerPrefab = PhotonNetwork.Instantiate(filePath, spawnPoint, Quaternion.identity);
    }

    
    private void OnInitializeLoadScene(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("로드 씬 호출");
        
        
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
        SceneManager.sceneLoaded -= OnInitializeLoadScene;
        Time.timeScale = 1f;
    }
}
