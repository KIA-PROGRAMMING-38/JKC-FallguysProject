using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ExitGames.Client.Photon;
using LiteralRepository;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class TestStageInitializer : MonoBehaviourPunCallbacks
{
    public override void OnJoinedRoom()
    {
        int index = 1;
        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            Hashtable playerIndex = new Hashtable() { { "PersonalIndex", index } }; 
            player.Value.SetCustomProperties(playerIndex);
            ++index;
        }
        
        Vector3 mapVector = new Vector3(0, 0, 0); 
        Quaternion mapRotation = Quaternion.Euler(0f, 180f,0f);

        SetPlayerSpawnPoints().Forget();
        
        if (!PhotonNetwork.IsMasterClient)
            return;
        
        PhotonNetwork.Instantiate
        (DataManager.SetDataPath
            (PathLiteral.Prefabs, "Stage", "FruitChute", "MapFruitChute"), mapVector, mapRotation);
    }

    private Vector3[] _spawnPoints =
    {
        new Vector3(0, 0, 0),
        new Vector3(-11, 0, -10), new Vector3(-5, 0, -10),
        new Vector3(5, 0, -10), new Vector3(11, 0, -10),
        new Vector3(-11, 0, -13), new Vector3(-5, 0, -13),
        new Vector3(-11, 0, -13), new Vector3(-5, 0, -13),
    };
    
    private async UniTaskVoid SetPlayerSpawnPoints()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(3f));
        
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
