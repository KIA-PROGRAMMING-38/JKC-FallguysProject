using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class Canon : MonoBehaviourPunCallbacks
{
    private FruitPooler _fruitPooler;
    private int _randomCreateFruitIndex;
    
    // 발사 위치와 각도를 설정하는 트랜스폼.
    private Transform _shootAngleTransform;
    // 오브젝트가 파괴될 경우 자원 정리를 위한 토큰.
    private CancellationTokenSource _cancellationTokenSource;
    private Animator _canonAnimator;

    [SerializeField]
    private float _minFiringDelay;
    [SerializeField]
    private float _maxFiringDelay;
    
    private float _firingDelay;
    
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
        TestAsync().Forget();
    }

    private async UniTaskVoid TestAsync()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(2f));

        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("InitiateFiring", RpcTarget.All);
        }
    }

    [PunRPC]
    public void InitiateFiring()
    {
        _firingDelay = Random.Range(_minFiringDelay, _maxFiringDelay);
        photonView.RPC("UpdateDelayTime", RpcTarget.All, _firingDelay);

        RepeatShootAnimation(_cancellationTokenSource.Token).Forget();
    }

    private void PlayAnimation()
    {
        _canonAnimator.Play("ShootMotion");
    }
    
    [PunRPC]
    public void UpdateDelayTime(float newDelayTime)
    {
        _firingDelay = newDelayTime;
    }

    private async UniTaskVoid RepeatShootAnimation(CancellationToken cancelToken)
    {
        while (true)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_firingDelay), cancellationToken: cancelToken);
            
            PlayAnimation();
        }
    }

    public void Shoot()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            string fruitType = SetDefaultPoolUsedString();
            Vector3 position = _shootAngleTransform.position;
            Quaternion rotation = Random.rotation;
            
            float delayTime = Random.Range(_minFiringDelay, _maxFiringDelay);
    
            photonView.RPC("UpdateDelayTime", RpcTarget.All, delayTime);
            photonView.RPC("ShootFruit", RpcTarget.All, fruitType, position, rotation);
        }
    }

    [PunRPC]
    public void ShootFruit(string fruitType, Vector3 position, Quaternion rotation)
    {
        Fruit fruit = _fruitPooler.defaultPrefabPool.Instantiate
            (fruitType, position, rotation).GetComponent<Fruit>();
       
        fruit.gameObject.SetActive(true);
        fruit.DefaultPool = _fruitPooler.defaultPrefabPool;

        fruit.transform.position = position;
        fruit.transform.rotation = rotation;
        Rigidbody fruitRigidbody = fruit.GetComponent<Rigidbody>();
        fruitRigidbody.velocity = Vector3.zero;
        fruitRigidbody.AddForce(-_shootAngleTransform.forward * _rejectionForce, ForceMode.VelocityChange);
    }

    private string[] randomFruitName =
        { "Fruit_Orange", "Fruit_Banana", "Fruit_Orange", "Fruit_Strawberry", "Fruit_Watermelon" };
    private string SetDefaultPoolUsedString()
    {
        int randomIndex = Random.Range(0, FruitPrefabRegistry.Repository.Count);
        
        return randomFruitName[randomIndex];
    }
    
    private void OnDestroy()
    {
        _cancellationTokenSource.Cancel();
    }
}
