using System;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;

public class LineEffectPooler : MonoBehaviour
{
    private LineEffectPool _lineEffectPool;
    private RespawnZone _spawnZone;
    private BoxCollider _spawnZoneCollider;

    private CancellationTokenSource _cancellationTokenSource;
    
    // SpawnZone의 위치 경계값들입니다.
    private float _colliderXMinPosition;
    private float _colliderXMaxPosition;
    private float _colliderYPosition;

    // 카메라의 앞쪽에 생성될 경우의 경계값들입니다. 
    private const float FRONT_X_MIN_POSITION = -3f; 
    private const float FRONT_X_MAX_POSITION = 3f;
    
    // 카메라로부터의 거리 상수들입니다.
    private const float FAR_FROM_THE_CAMERA = -1f;
    private const float NEAR_FROM_THE_CAMERA = -5f;
    
    // 생성 간 쿨다운을 제어하기 위한 상수들입니다.
    private const float SPAWN_COOLDOWN_TIME_MIN_VALUE = 0.2f;
    private const float SPAWN_COOLDOWN_TIME_MAX_VALUE = 0.5f;
    
    // 카메라에 대한 상대적인 생성 위치를 결정하기 위한 상수들입니다.
    private const int SPAWN_WEIGHT_MIN_VALUE = 0;
    private const int SPAWN_WEIGHT_MAX_VALUE = 4;

    private void Awake()
    {
        _lineEffectPool = new LineEffectPool(gameObject);
        _cancellationTokenSource = new CancellationTokenSource();
    }

    private void Start()
    {
        SpawnLineEffectsContinuously(_cancellationTokenSource.Token).Forget();
    }
    
    // 랜덤한 딜레이를 가지고 계속해서 LineEffect를 생성하는 UniTask입니다.
    private async UniTaskVoid SpawnLineEffectsContinuously(CancellationToken cancelToken)
    {
        while (true)
        {
            float randomSpawnCooldown = Random.Range
                (SPAWN_COOLDOWN_TIME_MIN_VALUE, SPAWN_COOLDOWN_TIME_MAX_VALUE);
            
            LineEffect lineEffect = _lineEffectPool.LineEffectPoolInstance.Get();
            lineEffect.transform.position = SetSpawnPosition();

            await UniTask.Delay(TimeSpan.FromSeconds(randomSpawnCooldown), cancellationToken: cancelToken);
        }
    }

    /// <summary>
    /// LineEffectPooler에서 사용될 객체들을 초기화합니다. 
    /// </summary>
    public void OnInitialize(RespawnZone respawnZone)
    {
        _spawnZone = respawnZone;
        _spawnZoneCollider = _spawnZone.GetComponent<BoxCollider>();
        _colliderXMinPosition = _spawnZoneCollider.bounds.min.x;
        _colliderXMaxPosition = _spawnZoneCollider.bounds.max.x;
        _colliderYPosition = _spawnZoneCollider.bounds.max.y;
    }

    private Vector3 SetSpawnPosition()
    {
        int randomNum = Random.Range(SPAWN_WEIGHT_MIN_VALUE, SPAWN_WEIGHT_MAX_VALUE);
        
        float zValue = SetZPostiionValue(randomNum);
        float xValue = SetXPositionValue(randomNum);
        
        Vector3 position = new Vector3(xValue, _colliderYPosition, zValue);

        return position;
    }

    private float SetZPostiionValue(int randomNum)
    {
        if (randomNum == SPAWN_WEIGHT_MIN_VALUE)
        {
            return NEAR_FROM_THE_CAMERA;
        }
        else
        {
            return FAR_FROM_THE_CAMERA;
        }
    }

    private float SetXPositionValue(int randomNum)
    {
        if (randomNum == SPAWN_WEIGHT_MIN_VALUE)
        {
            return Random.Range(FRONT_X_MIN_POSITION, FRONT_X_MAX_POSITION);
        }
        else
        {
            return Random.Range(_colliderXMinPosition, _colliderXMaxPosition);
        }
    }

    private void OnDestroy()
    {
        _cancellationTokenSource.Cancel();
    }
}
