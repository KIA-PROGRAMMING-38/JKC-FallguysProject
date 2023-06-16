using System;
using Cysharp.Threading.Tasks;
using LiteralRepository;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private void Start()
    {
        LoadGameLoadingScene().Forget(); 
    }

    private async UniTaskVoid LoadGameLoadingScene()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(10));

        SceneManager.LoadScene(PathLiteral.GameLoading);
    } 
}
