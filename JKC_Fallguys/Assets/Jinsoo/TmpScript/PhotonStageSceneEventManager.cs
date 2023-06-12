using LiteralRepository;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonStageSceneEventManager : MonoBehaviourPunCallbacks
{
    private PhotonStageSceneRoomManager _roomManager;
    
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
        OnInstantiatePhotonRoomManager();
        
        SceneManager.sceneLoaded += OnInitialzeLoadScene;
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
    
    private void OnInitialzeLoadScene(Scene scene, LoadSceneMode mode)
    {
        Player player = PhotonNetwork.LocalPlayer;
        object indexObject;

        if (player.CustomProperties.TryGetValue("PersonalIndex", out indexObject))
        {
            int index = (int)indexObject;
            string filePath = DataManager.SetDataPath(PathLiteral.Prefabs, "Player");
        
            Vector3 spawnPoint = _spawnPoints[index];
            PhotonNetwork.Instantiate(filePath, spawnPoint, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Failed to get the player index from custom properties.");
        }
    }
}
