using LiteralRepository;
using Photon.Pun;
using UnityEngine;

public class StageInstantiateManager : MonoBehaviourPun, IPunObservable
{
    private void Awake()
    {
        InitializeMap();
    }

    private void InitializeMap()
    {
        MapData mapData = StageDataManager.Instance.MapDatas[StageDataManager.Instance.MapPickupIndex];
    
        if (PhotonNetwork.IsMasterClient)
        {
            InstantiateMap(mapData);
        }
        
        InstantiatePlayer(mapData);
    }
    
    private void InstantiateMap(MapData mapData)
    {
        Debug.Log($"Map : {PhotonNetwork.LocalPlayer.ActorNumber}");
        
        string filePath = mapData.Data.PrefabFilePath;
        Vector3 mapPos = mapData.Data.MapPosition;
        Quaternion mapRota = mapData.Data.MapRotation;
        
        PhotonNetwork.Instantiate(filePath, mapPos, mapRota);
    }
    
    private void InstantiatePlayer(MapData mapData)
    {
        Debug.Log($"Player : {PhotonNetwork.LocalPlayer.ActorNumber}");
        
        string filePath = DataManager.SetDataPath(PathLiteral.Prefabs, TagLiteral.Player);
        Vector3 spawnPoint = mapData.Data.PlayerSpawnPosition[PhotonNetwork.LocalPlayer.ActorNumber];
    
        PhotonNetwork.Instantiate(filePath, spawnPoint, Quaternion.identity).GetComponent<PlayerReferenceManager>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }
}
