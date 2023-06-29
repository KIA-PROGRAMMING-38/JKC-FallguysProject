using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class HoopLegendAxe : MonoBehaviourPunCallbacks
{
    [SerializeField] 
    private float _minRotationSpeed;
    [SerializeField] 
    private float _maxRotationSpeed;
    
    private float _axeRotationSpeed;

    private Transform _axeTransform;
    private AxeController _axeController;
    private CancellationTokenSource _cancellationToken;

    private void Awake()
    {
        _axeTransform = transform.Find("AxeBody").GetComponent<Transform>();
        Debug.Assert(_axeTransform != null);
    }

    private void SetRotationForce()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            float rotationForce = Random.Range(_minRotationSpeed, _maxRotationSpeed);
            photonView.RPC("RpcAxeRotation", RpcTarget.All, rotationForce);
        }
    }
    
    private async UniTaskVoid TestAsync()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(5f));
    }

    [PunRPC]
    public void RpcAxeRotation(float rotationForce)
    {
        _axeRotationSpeed = rotationForce;
        AxeRotation(_cancellationToken).Forget();
    }

    public void Initialize(AxeController axeController, CancellationTokenSource cancelToken)
    {
        _axeController = axeController;
        transform.SetParent(_axeController.transform);
        _cancellationToken = cancelToken;
        
        TestAsync().Forget();

        SetRotationForce();
    }
    
    private async UniTask AxeRotation(CancellationTokenSource cancelToken)
    {
        while (true)
        {
            _axeTransform.Rotate(Vector3.forward * _axeRotationSpeed * Time.deltaTime);

            await UniTask.Yield(PlayerLoopTiming.Update, cancelToken.Token);
        }
    }

}
