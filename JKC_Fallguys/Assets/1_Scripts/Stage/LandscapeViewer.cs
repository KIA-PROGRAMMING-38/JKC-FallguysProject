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
        StageManager.Instance.StageDataManager.CurrentSequence
            .DistinctUntilChanged()
            .Subscribe(sequence => _camera.gameObject.SetActive(!(sequence == StageDataManager.StageSequence.GameInProgress || sequence == StageDataManager.StageSequence.PlayersReady)))
            .AddTo(this);
    }
}
