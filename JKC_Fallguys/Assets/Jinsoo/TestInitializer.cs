using System;
using Cysharp.Threading.Tasks;
using LiteralRepository;
using Photon.Pun;
using UnityEngine;

public class TestInitializer : SceneInitializer
{
    private PlayerPhotonController _player;

    private void Awake()
    {
        Screen.SetResolution(1080, 600, FullScreenMode.Windowed);
    }

    protected override void OnGetResources()
    {
        TestTask().Forget();
    }

    private async UniTaskVoid TestTask()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(3f));

        string pathString = DataManager.SetDataPath(PathLiteral.Prefabs, "Test", "Player");
        GameObject player = PhotonNetwork.Instantiate(pathString, transform.position, transform.rotation);
        Debug.Log("Player instantiated with PhotonView viewID: " + player.GetPhotonView().ViewID);
        _player = player.GetComponentInChildren<PlayerPhotonController>();
        if (_player == null) 
        {
            Debug.LogError("PlayerPhotonController component is missing in the instantiated object.");
            return;
        }
    }
}