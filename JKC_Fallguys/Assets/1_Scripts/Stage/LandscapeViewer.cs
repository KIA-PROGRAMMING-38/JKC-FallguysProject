using UniRx;
using UnityEngine;

public class LandscapeViewer : MonoBehaviour
{
    private Camera _camera;
    
    private void Awake()
    {
        _camera = GetComponent<Camera>();

        transform.position = StageManager.Instance.ObjectRepository
            .MapDatas[StageManager.Instance.ObjectRepository.MapPickupIndex.Value].Data.LandscapeViewPosition;
        transform.rotation = Quaternion.Euler(StageManager.Instance.ObjectRepository
            .MapDatas[StageManager.Instance.ObjectRepository.MapPickupIndex.Value].Data.LandscapeViewRotation);
    }

    private void Start()
    {
        InitializeRx();
    }

    private void InitializeRx()
    {
        StageManager.Instance.ObjectRepository.CurrentSequence
            .DistinctUntilChanged()
            .Subscribe(sequence => _camera.gameObject.SetActive(!(sequence == ObjectRepository.StageSequence.GameInProgress || sequence == ObjectRepository.StageSequence.PlayersReady)))
            .AddTo(this);
    }
}
