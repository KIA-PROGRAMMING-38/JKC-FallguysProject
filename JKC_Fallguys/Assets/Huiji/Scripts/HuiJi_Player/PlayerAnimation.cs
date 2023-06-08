using System;
using Cysharp.Threading.Tasks;
using Literal;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private PlayerInput _playerInput;
    private Rigidbody _playerRigidbody;
    
    [SerializeField] private float _acceleration = 0.5f;
    [SerializeField] private float _deceleration = 0.5f;
    [SerializeField] private float _topplingForce;
    
    private float _velocity;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerInput = GetComponentInParent<PlayerInput>();
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _playerInput.OnReleaseGrab -= ReleaseGrab;
        _playerInput.OnReleaseGrab += ReleaseGrab;
    }

    private void Update()
    {
        // Grab시 max값의 변수를 0.5로 변환하게 하는 로직 필요. 평소에는 max값이 1.
        // 위의 로직 함수를 Grab State에서 호출한다.
        
        if (_playerInput.InputVec.x != 0 || _playerInput.InputVec.z != 0)
        {
            _velocity += Time.deltaTime * _acceleration;
            _velocity = Mathf.Clamp(_velocity, 0, 1);
        }

        else
        {
            _velocity -= Time.deltaTime * _deceleration;
            _velocity = Mathf.Clamp(_velocity, 0, 1);
        }
        
        _animator.SetFloat(AnimLiteral.MOVESPEED, _velocity);
    }

    private bool _isGround;
    private void OnCollisionEnter(Collision collision)
    {
        // AttempingGrab중 GrabBox랑 닿으면 Grab로 전이
        if (collision.gameObject.CompareTag(TagLiteral.BOX) && _animator.GetBool(AnimLiteral.ISGRAB))
        {
            _animator.SetBool(AnimLiteral.ISGRABSUCCESS, true);
        }

        // Jump후 땅이랑 닿으면 Movement로 Exit.
        if (collision.gameObject.CompareTag(TagLiteral.GROUND) && _animator.GetBool(AnimLiteral.ISJUMPING))
        {
            _animator.SetBool(AnimLiteral.ISJUMPING, false);
        }

        if (collision.gameObject.CompareTag(TagLiteral.GROUND) && _animator.GetBool(AnimLiteral.ISDIVING))
        {
            _animator.SetBool(AnimLiteral.ISDIVING, false);
        }

        if (collision.gameObject.CompareTag(TagLiteral.GROUND))
        {
            _animator.SetBool(AnimLiteral.ISFALL, false);

            _isGround = true;
        }
        
        if (collision.impulse.magnitude > _topplingForce && collision.gameObject.CompareTag(TagLiteral.GROUND))
        {
            _animator.SetBool(AnimLiteral.ISFALL, true);
            CheckFallStateAfterDelay(delay: 0.5f).Forget();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag(TagLiteral.GROUND))
        {
            _isGround = false;
        }
    }

    // Ground에 닿았는데도 여전히 Fall상태일때 IsFall을 false로 만들어주는 함수.
    private async UniTaskVoid CheckFallStateAfterDelay(float delay)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(delay));

        if (_animator.GetBool(AnimLiteral.ISFALL) && _isGround)
        {
            _animator.SetBool(AnimLiteral.ISFALL, false);
        }
    }

    [SerializeField] private Transform _respawnPosition; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(TagLiteral.BOUNDARY))
        {
            _animator.SetBool(AnimLiteral.ISFALL, true);
        }

        if (other.gameObject.CompareTag(TagLiteral.RESPAWN))
        {
            transform.position = _respawnPosition.position;
            _animator.SetBool(AnimLiteral.ISRESPAWNING, true);
            _playerRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    private void ReleaseGrab()
    {
        _animator.SetBool(AnimLiteral.ISGRAB, false);
        _animator.SetBool(AnimLiteral.ISGRABSUCCESS, false);
    }
}
