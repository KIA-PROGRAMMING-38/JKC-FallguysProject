using UniRx;
using UnityEngine;

public class LandscapeViewer : MonoBehaviour
{
    private Camera _camera;
    
    private void Awake()
    {
        _camera = GetComponent<Camera>();

        transform.position = StageManager.Instance.StageDataManager
            .MapDatas[StageManager.Instance.StageDataManager.MapPickupIndex.Value].Data.LandscapeViewPosition;
        transform.rotation = Quaternion.Euler(StageManager.Instance.StageDataManager
            .MapDatas[StageManager.Instance.StageDataManager.MapPickupIndex.Value].Data.LandscapeViewRotation);
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
