using LiteralRepository;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Lobby씬을 관리하는 컴포넌트입니다.
/// </summary>
public class LobbySceneManager : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == SceneIndex.Lobby)
        {
            if (StageDataManager.Instance != null)
            {
                StageDataManager.Instance.DestorySelf();
            }
        }
    }
}
