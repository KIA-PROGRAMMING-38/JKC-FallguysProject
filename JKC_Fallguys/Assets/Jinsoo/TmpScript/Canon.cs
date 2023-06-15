using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Photon.Pun;
using UnityEngine;

public class Canon : MonoBehaviour
{
    private CanonController _canonController;
    private FruitPooler _fruitPooler;
    
    // 발사 위치와 각도를 설정하는 트랜스폼.
    private Transform _shootAngleTransform;
    // 오브젝트가 파괴될 경우 자원 정리를 위한 토큰.
    private CancellationTokenSource _cancellationTokenSource;
    private Animator _canonAnimator;    
    
    // 발사하는 힘을 나타내는 float 값.
    [SerializeField]
    private float _rejectionForce;

    private void Awake()
    {
        _shootAngleTransform = transform.Find("ShootingPoint").GetComponent<Transform>();
        _cancellationTokenSource = new CancellationTokenSource();
        _canonAnimator = GetComponent<Animator>();
    }

    public void Initialize(FruitPooler fruitPooler)
    {
        _fruitPooler = fruitPooler;
    }

    private void Start()
    {
        ReapeatShootAnimation(_cancellationTokenSource.Token).Forget();
    }
    
    private async UniTaskVoid ReapeatShootAnimation(CancellationToken cancelToken)
    {
        while (true)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(2f), cancellationToken: cancelToken);

            PlayAnimation();
        }
    }

    private void PlayAnimation()
    {
        _canonAnimator.Play("ShootMotion");
    }

    [PunRPC]
    public void ShootFruit()
    {
        Fruit fruitInstance = _fruitPooler.FruitPool.FruitPoolInstance.Get();
        fruitInstance.transform.position = _shootAngleTransform.position;
        fruitInstance.transform.rotation = _shootAngleTransform.rotation;
        Rigidbody fruitRigidbody = fruitInstance.GetComponentInChildren<Rigidbody>();
        fruitRigidbody.velocity = Vector3.zero;

        fruitRigidbody.AddForce(-_shootAngleTransform.forward * _rejectionForce, ForceMode.VelocityChange);
    }

    private void OnDestroy()
    {
        _cancellationTokenSource.Cancel();
    }
}
