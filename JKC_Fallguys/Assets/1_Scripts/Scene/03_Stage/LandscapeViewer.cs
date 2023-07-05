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
        StageDataManager.Instance.IsGameActive
            .DistinctUntilChanged()
            .Where(isGameActive => !isGameActive)
            .Subscribe(_ => _camera.gameObject.SetActive(true))
            .AddTo(this);
        
        StageDataManager.Instance.IsGameStart
            .DistinctUntilChanged()
            .Where(isGameStarted => isGameStarted)
            .Subscribe(_ => _camera.gameObject.SetActive(false))
            .AddTo(this);
    }
}
