using LiteralRepository;
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
        string filePath = ResourceManager.SetDataPath(PathLiteral.Prefabs, TagLiteral.Player);
        Vector3 spawnPoint = mapData.Data.PlayerSpawnPosition[PhotonNetwork.LocalPlayer.ActorNumber];
        PlayerPhotonController playerPhotonController =
            PhotonNetwork.Instantiate(filePath, spawnPoint, Quaternion.identity)
                .GetComponentInChildren<PlayerPhotonController>();

        playerPhotonController.photonView.RPC
            ("RpcSetTextureIndex", RpcTarget.AllBuffered, ResourceManager.PlayerTextureIndex.Value);
        playerPhotonController.photonView.RPC
            ("RpcSetInitialize", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.ActorNumber, PhotonNetwork.LocalPlayer.NickName);

        playerPhotonController.transform.root.gameObject.transform
            .SetParent(StageDataManager.Instance.gameObject.transform);
    }
}
