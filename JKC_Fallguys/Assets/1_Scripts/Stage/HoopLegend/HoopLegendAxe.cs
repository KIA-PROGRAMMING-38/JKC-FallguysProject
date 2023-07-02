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
    
    public void Initialize(AxeController axeController, CancellationTokenSource cancelToken)
    {
        _axeController = axeController;
        transform.SetParent(_axeController.transform);
        _cancellationToken = cancelToken;
    }

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RpcInitiateRotation", RpcTarget.All);
        }
    }

    [PunRPC]
    public void RpcInitiateRotation()
    {
        float rotationForce = Random.Range(_minRotationSpeed, _maxRotationSpeed);
        photonView.RPC("RpcSetAxeRotationForce", RpcTarget.All, rotationForce);
        
        AxeRotation(_cancellationToken).Forget();
    }

    [PunRPC]
    public void RpcSetAxeRotationForce(float rotationForce)
    {
        _axeRotationSpeed = rotationForce;
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
