using System.IO;
using LiteralRepository;
using Model;
using Photon.Pun;
using UnityEngine;

public class StageInstantiateManager : MonoBehaviourPun
{
    private void Awake()
    {
        InitializeMap();
    }

    private void InitializeMap()
    {
        MapData mapData = StageDataManager.Instance.MapDatas[StageDataManager.Instance.MapPickupIndex.Value];

        if (PhotonNetwork.IsMasterClient)
        {
            InstantiateMap(mapData);
        }

        InstantiatePlayer(mapData);
    }

    private void InstantiateMap(MapData mapData)
    {
        string filePath = mapData.Data.PrefabFilePath;
        Vector3 mapPos = mapData.Data.MapPosition;
        Quaternion mapRota = mapData.Data.MapRotation;

        PhotonNetwork.Instantiate(filePath, mapPos, mapRota);


    }

    private void InstantiatePlayer(MapData mapData)
    {
        string filePath = Path.Combine(PathLiteral.Prefabs, TagLiteral.Player);
        Vector3 spawnPoint = mapData.Data.PlayerSpawnPosition[PhotonNetwork.LocalPlayer.ActorNumber];
        PlayerPhotonController playerPhotonController =
            PhotonNetwork.Instantiate(filePath, spawnPoint, Quaternion.identity)
                .GetComponentInChildren<PlayerPhotonController>();

        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        string playerName = PhotonNetwork.LocalPlayer.NickName;
        int playerTextureIndex = LobbySceneModel.PlayerTextureIndex.Value;

        playerPhotonController.photonView.RPC
            ("RpcSetInitialize", RpcTarget.AllBuffered, actorNumber, playerName, playerTextureIndex);

        playerPhotonController.transform.root.gameObject.transform.SetParent(StageDataManager.Instance.gameObject.transform);
    }

}
