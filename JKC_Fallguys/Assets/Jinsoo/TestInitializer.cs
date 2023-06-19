using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using LiteralRepository;
using Photon.Pun;
using UnityEngine;

public class TestInitializer : SceneInitializer
{
    private List<Texture2D> _playerTexture;

    private PlayerPhotonController _player;

    private void Awake()
    {
        _playerTexture = new List<Texture2D>(Resources.LoadAll<Texture2D>
            (DataManager.SetDataPath("Textures", "PlayerTexture")));
    }

    int index = 0;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log(index);
            
            if (index < _playerTexture.Count)
            {
                _player._bodyMeshRenderer.material.mainTexture = _playerTexture[index];
                ++index;    
            }
            else
            {
                index = 0;
                _player._bodyMeshRenderer.material.mainTexture = _playerTexture[index];
            }
        }
    }


    protected override void OnGetResources()
    {
        TestTask().Forget();
    }

    private async UniTaskVoid TestTask()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(5f));

        string pathString = DataManager.SetDataPath(PathLiteral.Prefabs, "Test", "Player");
        _player = PhotonNetwork.Instantiate(pathString, transform.position, transform.rotation)
            .transform.Find("Character").GetComponent<PlayerPhotonController>();
    }
}
