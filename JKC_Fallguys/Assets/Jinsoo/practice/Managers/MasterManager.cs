using System.Collections.Generic;
using Photon.Pun;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Singletons/MasterManager")]
public class MasterManager : SingletonscriptableObject<MasterManager>
{
    [SerializeField] private GameSettings _gameSettings;
    public static GameSettings GameSettings
    {
        get { return Instance._gameSettings; }
    }

    private List<NetworkedPrefab> _networkedPrefabs = new List<NetworkedPrefab>();
    
    public static GameObject NetworkInstantiate(GameObject obj, Vector3 position, Quaternion rotation)
    {
        foreach (NetworkedPrefab elem in Instance._networkedPrefabs)
        {
            Debug.Log(elem.Prefab.name);
        }
        
        foreach (NetworkedPrefab networkedPrefab in Instance._networkedPrefabs)
        {
            if (networkedPrefab.Prefab == obj)
            {
                GameObject result = PhotonNetwork.Instantiate(networkedPrefab.Path, position, rotation);
                return result;
            }
            else
            {
                Debug.LogError("Path is empty for gameobject name " + networkedPrefab.Prefab);
                return null;
            }
        }
        
        return null;
    }
}