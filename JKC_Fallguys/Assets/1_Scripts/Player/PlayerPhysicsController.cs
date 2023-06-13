using System;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// 플레이어 물리 움직임을 담당한다.
/// </summary>
public class PlayerPhysicsController : MonoBehaviour
{
    private Rigidbody _playerRigidbody;
    private PlayerInput _playerInput;
    private CameraAngle _camera;
    
    [SerializeField] private float _diveForce;
    [SerializeField] private float _jumpForce;
    
    private Vector3 _zeroVec = Vector3.zero;
    [SerializeField] private float _rotSpeed;
    [SerializeField] private float _moveSpeed;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerInput = GetComponentInParent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        
    }

    public void BindCameraAngle(CameraAngle cameraAngle)
    {
        _camera = cameraAngle;
    }

    private void Start()
    {
        _playerInput.OnMovement -= CurrentMoveDirection;
        _playerInput.OnMovement += CurrentMoveDirection;
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
        _moveDir = _forwardAngleVec * _playerInput.InputVec.z + _rightAngleVec * _playerInput.InputVec.x;
    }

    /// <summary>
    /// Movement State에서 호출.
    /// </summary>
    public void Move()
    {
        CheckGround();
        
        if (_playerInput.InputVec != _zeroVec && _playerInput.CannotMove == false)
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
        int layerMask = 1 << LayerMask.NameToLayer("Ground");
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
    /// <summary>
    /// Jumping State에서 호출.
    /// </summary>
    public void OnJumping()
    {
        if (_playerInput.InputVec != _zeroVec && _playerInput.CannotMove == false)
        {
            _playerRigidbody.AddForce(_moveDir * _jumpMovementForce, ForceMode.Force);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_moveDir), _rotSpeed * Time.deltaTime);    
        }
    }

    /// <summary>
    /// Jump Start State에서 호출.
    /// </summary>
    public void ActivateJumpAction()
    {
        JumpAction().Forget();
    }

    // 위로 Jump한다.
    private async UniTaskVoid JumpAction()
    {
        _playerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        await UniTask.Yield();
    }

    /// <summary>
    /// DiveStartState에서 호출.
    /// </summary>
    public void ActivateDiveAction()
    {
        DiveRotation().Forget();
        DiveAction().Forget();
    }
    
    private Vector3 _currentRotation;
    private Quaternion _targetRotation;
    
    private float _diveRotationX = 90;
    [SerializeField] private float _diveRotationSpeed;
    
    // 캐릭터가 Dive할때 회전하는 함수.
    private async UniTaskVoid DiveRotation()
    {
        _playerInput.CannotMove = true;
        
        _currentRotation = transform.rotation.eulerAngles;
        _targetRotation = Quaternion.Euler(_diveRotationX, _currentRotation.y, _currentRotation.z);
        
        while (Quaternion.Angle(transform.rotation, _targetRotation) > 0.1f)
        {
            // 캐릭터의 원래 방향으로 rotation한다.
            float step = _diveRotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, step);
            
            await UniTask.Yield();
        }
        
        _playerInput.CannotMove = false;
    }

    private Vector3 _playerDirection;
    private Vector3 _diveDirection;
    [SerializeField] private float _diveHeightDirection;
    
    // Dive시 앞으로 힘을 주어 점프하는 함수.
    private async UniTaskVoid DiveAction()
    {
        await UniTask.DelayFrame(3);
        
        // Player가 바라보고 있는 방향을 구한뒤 Dive Direction을 구한다.
        _playerDirection = transform.forward;
        _diveDirection = new Vector3(_playerDirection.x, _diveHeightDirection, _playerDirection.z);

        // Dive Direction으로 힘을 준다.
        _playerRigidbody.AddForce(_diveDirection * _diveForce, ForceMode.Impulse);
    }

    /// <summary>
    /// GetUpState에서 호출.
    /// </summary>
    public void ActivateGetUp()
    {
        GetUp().Forget();
    }
    
    [SerializeField] private float _getUpRotationSpeed;

    // 캐릭터가 Dive이후 일어나게 하는 함수.
    private async UniTaskVoid GetUp()
    {
        _playerInput.CannotMove = true;
        
        _currentRotation = transform.rotation.eulerAngles;
        _targetRotation = Quaternion.Euler(0, _currentRotation.y, _currentRotation.z);
        
        while (Quaternion.Angle(transform.rotation, _targetRotation) > 0.1f)
        {
            // 캐릭터의 원래 방향으로 rotation한다.
            float step = _getUpRotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, step);
            
            await UniTask.Yield();
        }
        
        _playerInput.CannotMove = false;
    }

    /// <summary>
    /// 넘어지면서 회전축을 푸는 함수. Fall State에서 호출.
    /// </summary>
    public void UnfreezeRotationAxis()
    {
        _playerRigidbody.constraints = RigidbodyConstraints.None;
    }

    /// <summary>
    /// Recovery State에서 호출.
    /// </summary>
    public void ActivateRecovery()
    {
        Recovery().Forget();
    }
    
    // 평지에서 Fall 이후 다시 일어나게 하는 함수.
    private async UniTaskVoid Recovery()
    {
        _playerInput.CannotMove = true;
        
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        
        _currentRotation = transform.rotation.eulerAngles;
        _targetRotation = Quaternion.Euler(0, _currentRotation.y, 0);
        
        _playerRigidbody.constraints = RigidbodyConstraints.FreezeRotation;

        while (Quaternion.Angle(transform.rotation, _targetRotation) > 0.1f)
        {
            // 캐릭터의 원래 방향으로 rotation한다.
            float step = _getUpRotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, step);
            
            await UniTask.Yield();
        }
        
        _playerInput.CannotMove = false;
    }

    [SerializeField] private Transform _respawnPosition; 
    /// <summary>
    /// Fall 이후 다시 원위치 되는 함수. Respawn Boundary랑 충돌햇을때 호출. 
    /// </summary>
    public void Respawn()
    {
        transform.position = _respawnPosition.position;
        _playerRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }
}
