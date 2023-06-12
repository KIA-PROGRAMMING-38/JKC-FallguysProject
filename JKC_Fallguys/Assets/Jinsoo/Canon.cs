using System;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using LiteralRepository;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Canon : MonoBehaviour
{
    private Transform _shootAngleTransform;
    private CancellationTokenSource _cancellationTokenSource;
    private Animator _canonAnimator;    
    
    [SerializeField]
    private float _rejectionForce;

    private void Awake()
    {
        _shootAngleTransform = transform.Find("ShootingPoint").GetComponent<Transform>();
        _cancellationTokenSource = new CancellationTokenSource();
        _canonAnimator = GetComponent<Animator>();

        GetFruitsData();
    }

    private void GetFruitsData()
    {
        string[] fruits = new string[] { "Apple", "Banana", "Orange", "Strawberry", "Watermelon" };
        
        for (int i = 0; i < fruits.Length; i++)
        {
            string fruitPath = DataManager.SetDataPath(PathLiteral.Prefabs, "FruitChute", "Fruits", fruits[i]);
            Fruit fruit = Resources.Load<Fruit>(fruitPath);

            if (!FruitPrefabRegistry.Repository.ContainsKey(i))
            {
                FruitPrefabRegistry.Repository.Add(i, fruit);
            }
        }
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

    public void ShootFruit()
    {
        Fruit fruitInstance = Instantiate(FruitSelection(), _shootAngleTransform.position, _shootAngleTransform.rotation);
        Rigidbody fruitRigidbody = fruitInstance.GetComponentInChildren<Rigidbody>();

        fruitRigidbody.AddForce(-_shootAngleTransform.forward * _rejectionForce, ForceMode.VelocityChange);
    }

    private Fruit FruitSelection()
    {
        Fruit fruit = default;
        int randomFruitPickNumber = Random.Range(0, 5);

        switch (randomFruitPickNumber)
        {
            case 0:
                fruit = FruitPrefabRegistry.Repository[FruitPrefabRegistry.Apple];
                break;
            case 1:
                fruit = FruitPrefabRegistry.Repository[FruitPrefabRegistry.Banana];
                break;
            case 2:
                fruit = FruitPrefabRegistry.Repository[FruitPrefabRegistry.Orange];
                break;
            case 3:
                fruit = FruitPrefabRegistry.Repository[FruitPrefabRegistry.StrawBerry];
                break;
            case 4:
                fruit = FruitPrefabRegistry.Repository[FruitPrefabRegistry.WaterMelon];
                break;
            default:
                Debug.LogError($"The value of the key is invalid. The value of the key is {randomFruitPickNumber}");
                break;
        }

        return fruit;
    }

    private void OnDestroy()
    {
        _cancellationTokenSource.Cancel();
    }
}
