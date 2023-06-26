using UniRx;
using UnityEngine;

public class JumpClubMapInitializer : MonoBehaviour
{
    private Camera _observeCamera;

    private void Awake()
    {
        _observeCamera = transform.Find("ObserveCamera").GetComponent<Camera>();
        Debug.Assert(_observeCamera != null);

        InitialzeRx();
    }

    private void InitialzeRx()
    {
        StageDataManager.Instance.IsPlayerAlive
            .DistinctUntilChanged()
            .Where(alive => !alive)
            .Subscribe(_ => _observeCamera.gameObject.SetActive(true));
    }
}
