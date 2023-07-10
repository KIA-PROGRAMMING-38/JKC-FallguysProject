using UniRx;
using UnityEngine;

public class LandscapeViewer : MonoBehaviour
{
    private Camera _camera;
    
    private void Awake()
    {
        _camera = transform.Find("Camera").GetComponent<Camera>();
    }

    private void Start()
    {
        InitializeRx();
    }

    private void InitializeRx()
    {
        StageManager.Instance.StageDataManager.IsGameActive
            .DistinctUntilChanged()
            .Where(isGameActive => !isGameActive)
            .Subscribe(_ => _camera.gameObject.SetActive(true))
            .AddTo(this);
        
        StageManager.Instance.StageDataManager.IsGameStart
            .DistinctUntilChanged()
            .Where(isGameStarted => isGameStarted)
            .Subscribe(_ => _camera.gameObject.SetActive(false))
            .AddTo(this);
    }
}
