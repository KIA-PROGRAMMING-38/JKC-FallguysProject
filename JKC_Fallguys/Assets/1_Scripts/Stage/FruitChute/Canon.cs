using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class Canon : MonoBehaviourPunCallbacks
{
    private PhotonView _photonView;
    private FruitPooler _fruitPooler;
    private int _randomCreateFruitIndex;
    
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
        _photonView = GetComponent<PhotonView>();
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
            await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: cancelToken);
            PlayAnimation();
        }
    }

    private void PlayAnimation()
    {
        _canonAnimator.Play("ShootMotion");
    }
    
    public void Shoot()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            string fruitType = SetDefaultPoolUsedString();
            Vector3 position = _shootAngleTransform.position;
            Quaternion rotation = _shootAngleTransform.rotation;
            
            _photonView.RPC("ShootFruit", RpcTarget.All, fruitType, position, rotation);
        }
    }

    [PunRPC]
    public void ShootFruit(string fruitType, Vector3 position, Quaternion rotation)
    {
        Debug.Log("ShootFruit");

        if (_fruitPooler.defaultPrefabPool == null)
        {
            Debug.Log("defaultPool is null");
            Debug.Break();
        }

        if (_fruitPooler == null)
        {
            Debug.Log("fruitpooler is null");
            Debug.Break();
        }
        
        
        
        Fruit fruit = _fruitPooler.defaultPrefabPool.Instantiate
            (fruitType, position, rotation).GetComponent<Fruit>();

        if (fruit == null)
        {
            Debug.Log("fruit is null");
            Debug.Break();
        }
        
        fruit.gameObject.SetActive(true);
        fruit.DefaultPool = _fruitPooler.defaultPrefabPool;

        fruit.transform.position = position;
        fruit.transform.rotation = rotation;
        Rigidbody fruitRigidbody = fruit.GetComponent<Rigidbody>();
        fruitRigidbody.velocity = Vector3.zero;
        fruitRigidbody.AddForce(-_shootAngleTransform.forward * _rejectionForce, ForceMode.VelocityChange);
    }

    private string SetDefaultPoolUsedString()
    {
        int randomIndex = Random.Range(0, FruitPrefabRegistry.Repository.Count);
        string str = default;

        switch (randomIndex)
        {
            case 0:
                str = "Fruit_Orange";
                break;
            case 1:
                str = "Fruit_Banana";
                break;
            case 2:
                str = "Fruit_Orange";
                break;
            case 3:
                str = "Fruit_Strawberry";
                break;
            case 4:
                str = "Fruit_Watermelon";
                break;
        }

        return str;
    }
    
    private void OnDestroy()
    {
        _cancellationTokenSource.Cancel();
    }
}
