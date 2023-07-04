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
            .Where(status => !StageDataManager.Instance.IsGameActive.Value)
            .Subscribe(_ => gameObject.SetActive(true))
            .AddTo(this);
        
        StageDataManager.Instance.IsGameActive
            .DistinctUntilChanged()
            .Where(status => StageDataManager.Instance.IsGameActive.Value)
            .Subscribe(_ => gameObject.SetActive(false))
            .AddTo(this);
    }
}
