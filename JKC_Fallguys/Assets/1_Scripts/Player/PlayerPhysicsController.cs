using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using LiteralRepository;
using Photon.Pun;
using UnityEngine;

/// <summary>
/// 플레이어 물리 움직임을 담당한다.
/// </summary>
public class PlayerPhysicsController : MonoBehaviourPun
{
    private Rigidbody _playerRigidbody;
    private PlayerInputController _playerInputController;
    private CameraAngle _camera;
    private CancellationTokenSource _cts = new CancellationTokenSource();
    
    [SerializeField] private float _diveForce;
    [SerializeField] private float _jumpForce;
    
    private Vector3 _zeroVec = Vector3.zero;
    [SerializeField] private float _rotSpeed;
    [SerializeField] private float _moveSpeed;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerInputController = GetComponentInParent<PlayerInputController>();
    }

    public void BindCameraAngle(CameraAngle cameraAngle)
    {
        _camera = cameraAngle;
    }

    public void ResetCameraAngle()
    {
        _camera.transform.rotation = Quaternion.Euler(345, 0, 0);
    }

    private void Start()
    {
        _playerInputController.OnMovement -= CurrentMoveDirection;
        _playerInputController.OnMovement += CurrentMoveDirection;

        _priviousDiveHeightForce = _diveHeightForce;
    }
    
    private Vector3 _forwardAngleVec;
    private Vector3 _rightAngleVec;
    private Vector3 _moveDir;
    
    // 현재 카메라 시야 기준으로 x, z축 판별 식
    // moveDir를 이용하여 플레이어가 움직인다
    private void CurrentMoveDirection()
    {
        _forwardAngleVec = new Vector3(_camera.transform.forward.x, 0f, _camera.transform.forward.z).normalized;
        _rightAngleVec = new Vector3(_camera.transform.right.x, 0f, _camera.transform.right.z).normalized;
        _moveDir = _forwardAngleVec * _playerInputController.InputVec.z + _rightAngleVec * _playerInputController.InputVec.x;
    }

    /// <summary>
    /// Movement State에서 호출.
    /// </summary>
    public void Move()
    {
        if (!photonView.IsMine || StageManager.Instance.StageDataManager.CurrentSequence.Value != StageDataManager.StageSequence.GameInProgress)
            return;
        
        CheckGround();
        
        if (_playerInputController.InputVec != _zeroVec && _playerInputController.CannotMove == false)
        {
            Vector3 testVec = new Vector3(_moveDir.x, _moveDir.y, _moveDir.z);
            _playerRigidbody.velocity = testVec * _moveSpeed;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_moveDir), _rotSpeed * Time.deltaTime);
        }
    }

    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private float _castRadius;
    [SerializeField] private float _groundCheckDistance;
    
    private void CheckGround()
    {
        // 초기 슬로프 각도 설정.
        float groundSlopeAngle = 0f;
        // 지면과의 충돌 여부 확인.
        int layerMask = 1 << LayerMask.NameToLayer(TagLiteral.Ground);
        bool cast = Physics.SphereCast
        (_groundCheckPoint.position, _castRadius, Vector3.down, out var hit, 
            _groundCheckDistance, layerMask);
            
        if (cast)
        {
            // 지면의 법선 벡터를 구하고, 그 벡터와 수직축과의 벡터를 구함.
            Vector3 groundNormal = hit.normal;
            // 지면 벡터와 수직축을 교차한 벡터를 구한다.
            // 슬로프의 방향을 얻을 수 있다.
            groundSlopeAngle = Vector3.Angle(groundNormal, Vector3.up);
        }
        
        // 슬로프의 방향을 계산한다.
        Vector3 slopeDirection = Vector3.Cross(hit.normal, Vector3.Cross(Vector3.up, hit.normal));
        
        // 슬로프 각도를 -1과 1사이의 값으로 변환한다.
        float slopeFactor = groundSlopeAngle / 90f;
        
        // 플레이어가 내리막을 향한다면 음수로 설정한다.
        if (Vector3.Dot(_moveDir, slopeDirection) < 0) 
        {
            slopeFactor *= -1;
        }
        
        _moveDir.y = slopeFactor;
    }
 
    [SerializeField] private float _jumpMovementForce;
    public CancellationTokenSource jumpCancellationTokenSource;
    CancellationToken _jumpCancellationToken => jumpCancellationTokenSource.Token;
    
    /// <summary>
    /// UniTask함수인 OnJumpingAction을 호출하는 함수입니다. JumpingState에서 호출해주세요.
    /// </summary>
    public void ActivateOnJumpingAction()
    {
        jumpCancellationTokenSource = new CancellationTokenSource();
        OnJumpingActionAsync().Forget();
    }

    /// <summary>
    /// 점프상태중에 인풋 방향대로 움직이기 위한 함수입니다.
    /// </summary>
    /// <returns></returns>
    private async UniTaskVoid OnJumpingActionAsync()
    {
        if (!photonView.IsMine || StageManager.Instance.StageDataManager.CurrentSequence.Value != StageDataManager.StageSequence.GameInProgress)
            return;

        while ( !_jumpCancellationToken.IsCancellationRequested )
        {
            if ( _playerInputController.InputVec != _zeroVec && _playerInputController.CannotMove == false )
            {
                _playerRigidbody.AddForce( _moveDir * _jumpMovementForce, ForceMode.Force );
                transform.rotation = Quaternion.Lerp( transform.rotation, Quaternion.LookRotation( _moveDir ), _rotSpeed * Time.deltaTime );
            }

            if ( _jumpCancellationToken.IsCancellationRequested )
            {
                break;
            }
            
            await UniTask.Yield( PlayerLoopTiming.FixedUpdate );
        }
    }

    /// <summary>
    /// UniTask함수인 JumpAction을 호출하는 함수입니다. Jump Start State에서 호출해주세요.
    /// </summary>
    public void ActivateJumpAction()
    {
        if (!photonView.IsMine)
            return;
        
        JumpStartActionAsync().Forget();
    }

    // 위로 Jump한다.
    private async UniTaskVoid JumpStartActionAsync()
    {
        _playerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        await UniTask.Yield( PlayerLoopTiming.FixedUpdate );
    }

    /// <summary>
    /// UniTask로 구현된 Dive관련 함수들을 호출하는 함수입니다. DiveStartState에서 호출해주세요.
    /// </summary>
    public void ActivateDiveAction()
    {
        if (!photonView.IsMine || StageManager.Instance.StageDataManager.CurrentSequence.Value != StageDataManager.StageSequence.GameInProgress)
            return;
        
        DiveRotationAsync().Forget();
        DiveActionAsync().Forget();
    }
    
    private Vector3 _currentRotation;
    private Quaternion _targetRotation;
    
    private float _diveRotationX = 90;
    [SerializeField] private float _diveRotationSpeed;
    
    // 캐릭터가 Dive할때 회전하는 함수입니다.
    private async UniTaskVoid DiveRotationAsync()
    {
        _playerInputController.CannotMove = true;
        
        _currentRotation = transform.rotation.eulerAngles;
        _targetRotation = Quaternion.Euler(_diveRotationX, _currentRotation.y, _currentRotation.z);
        
        while (Quaternion.Angle(transform.rotation, _targetRotation) > 0.1f)
        {
            float step = _diveRotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, step);
            
            await UniTask.Yield(cancellationToken: _cts.Token);
        }
        
        _playerInputController.CannotMove = false;
    }

    private Vector3 _playerDirection;
    private Vector3 _diveDirection;
    [SerializeField] private float _diveHeightForce;
    [SerializeField] private float _diveForwardForce;
    private float _priviousDiveHeightForce;

    public void ZeroizeDiveHeightForce()
    {
        _diveHeightForce = 0f;
    }

    public void RestoreDiveHeightForce()
    {
        _diveHeightForce = _priviousDiveHeightForce;
    }

    // Dive시 앞으로 힘을 주어 움직이게 하는 함수 입니다.
    private async UniTaskVoid DiveActionAsync()
    {
        // Player가 바라보고 있는 방향을 구한뒤 Dive Direction을 구합니다.
        _diveDirection = transform.forward * _diveForwardForce + transform.up * _diveHeightForce;

        // Dive Direction으로 힘을 줍니다.
        _playerRigidbody.AddForce(_diveDirection * _diveForce, ForceMode.Impulse);

        await UniTask.Yield(PlayerLoopTiming.FixedUpdate, cancellationToken: _cts.Token);
    }

    /// <summary>
    /// GetUpState에서 호출해주세요.
    /// </summary>
    public void ActivateGetUp()
    {
        if (!photonView.IsMine || StageManager.Instance.StageDataManager.CurrentSequence.Value != StageDataManager.StageSequence.GameInProgress)
            return;
        
        GetUpAsync().Forget();
    }
    
    [SerializeField] private float _getUpRotationSpeed;

    // 캐릭터가 Dive이후 일어나게 하는 함수입니다.
    private async UniTaskVoid GetUpAsync()
    {
        _playerInputController.CannotMove = true;
        
        _currentRotation = transform.rotation.eulerAngles;
        _targetRotation = Quaternion.Euler(0, _currentRotation.y, _currentRotation.z);
        
        while (Quaternion.Angle(transform.rotation, _targetRotation) > 0.1f)
        {
            float step = _getUpRotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, step);
            
            await UniTask.Yield(cancellationToken: _cts.Token);
        }
        
        _playerInputController.CannotMove = false;
    }

    /// <summary>
    /// 넘어지면서 회전축을 푸는 함수입니다. Fall State에서 호출해주세요.
    /// </summary>
    public void UnfreezeRotationAxis()
    {
        if (!photonView.IsMine)
            return;
        
        _playerRigidbody.constraints = RigidbodyConstraints.None;
    }

    /// <summary>
    /// Recovery State에서 호출해주세요.
    /// </summary>
    public void ActivateRecovery()
    {
        if (!photonView.IsMine)
            return;
        
        RecoveryAsync().Forget();
    }
    
    // 평지에서 Fall 이후 다시 일어나게 하는 함수입니다.
    private async UniTaskVoid RecoveryAsync()
    {
        _playerInputController.CannotMove = true;
        
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        
        _currentRotation = transform.rotation.eulerAngles;
        _targetRotation = Quaternion.Euler(0, _currentRotation.y, 0);
        
        _playerRigidbody.constraints = RigidbodyConstraints.FreezeRotation;

        while (Quaternion.Angle(transform.rotation, _targetRotation) > 0.1f)
        {
            float step = _getUpRotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, step);
            
            await UniTask.Yield(cancellationToken: _cts.Token);
        }
        
        _playerInputController.CannotMove = false;
    }

    /// <summary>
    /// Fall 이후 다시 Respawn 되는 함수입니다.. Respawn Boundary랑 충돌햇을때 호출해주세요. 
    /// </summary>
    public void Respawn(Vector3 respawnPos, Quaternion respawnAngle)
    {
        if (!photonView.IsMine || StageManager.Instance.StageDataManager.CurrentSequence.Value != StageDataManager.StageSequence.GameInProgress)
            return;

        transform.position = respawnPos;
        transform.rotation = respawnAngle;
        ResetCameraAngle();
        _playerRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void OnDestroy()
    {
        if (jumpCancellationTokenSource != null)
            jumpCancellationTokenSource.Cancel();
        
        if (_cts != null)
            _cts.Cancel();
    }
}
