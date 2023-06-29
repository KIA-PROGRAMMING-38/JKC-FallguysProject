using System;
using LiteralRepository;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestManager : MonoBehaviour
{
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // if (scene.buildIndex != SceneIndex.Lobby && scene.buildIndex != SceneIndex.MatchingStandby)
        // {
        //     if (AudioManager.Instance != null)
        //     {
        //         AudioManager.Instance.DestorySelf();
        //     }
        // }
    }
}
