using System;
using Cysharp.Threading.Tasks;
using LiteralRepository;
using Photon.Pun;
using ResourceRegistry;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAnimation : MonoBehaviourPun
{
    private Animator _animator;
    private PlayerInput _playerInput;
    private AudioSource _audioSource;

    // 플레이어 데이터로 변환 필요.
    [SerializeField] private float _acceleration = 0.5f;
    [SerializeField] private float _deceleration = 0.5f;
    [SerializeField] private float _topplingForce;

    private float _velocity;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerInput = GetComponentInParent<PlayerInput>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if ( !photonView.IsMine )
            return;

        _playerInput.OnReleaseGrab -= ReleaseGrab;
        _playerInput.OnReleaseGrab += ReleaseGrab;
    }

    private void Update()
    {
        if ( !photonView.IsMine || !StageDataManager.Instance.IsGameActive.Value )
            return;

        // Grab시 max값의 변수를 0.5로 변환하게 하는 로직 필요. 평소에는 max값이 1.
        // 위의 로직 함수를 Grab State에서 호출한다.

        if ( _playerInput.InputVec.x != 0 || _playerInput.InputVec.z != 0 )
        {
            _velocity += Time.deltaTime * _acceleration;
            _velocity = Mathf.Clamp( _velocity, 0, 1 );
        }

        else
        {
            _velocity -= Time.deltaTime * _deceleration;
            _velocity = Mathf.Clamp( _velocity, 0, 1 );
        }

        _animator.SetFloat( AnimLiteral.MoveSpeed, _velocity );

        CheckPlatform();
    }


    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private float _groundCheckDistance;
    private void CheckPlatform()
    {
        int layerMask = 1 << LayerMask.NameToLayer( TagLiteral.Ground );

        bool hit = Physics.Raycast( _groundCheckPoint.position, Vector3.down, _groundCheckDistance, layerMask );
        if ( hit )
        {
            _playerInput.IsNothingUnderfoot = false;
        }
        else
        {
            _playerInput.IsNothingUnderfoot = true;
        }
    }

    private bool _isGround;
    private void OnCollisionEnter( Collision collision )
    {
        if ( !photonView.IsMine )
            return;

        // AttempingGrab중 GrabBox랑 닿으면 Grab로 전이
        if ( collision.gameObject.CompareTag( TagLiteral.Box ) && _animator.GetBool( AnimLiteral.IsGrab ) )
        {
            _animator.SetBool( AnimLiteral.IsGrabSuccess, true );
        }

        // Jump후 땅이랑 닿으면 Movement로 Exit.
        if ( collision.gameObject.CompareTag( TagLiteral.Ground ) && _animator.GetBool( AnimLiteral.IsJumping ) )
        {
            _animator.SetBool( AnimLiteral.IsJumping, false );
        }

        if ( collision.gameObject.CompareTag( TagLiteral.Ground ) && _animator.GetBool( AnimLiteral.IsDiving ) )
        {
            _animator.SetBool( AnimLiteral.IsDiving, false );
        }

        if ( collision.gameObject.CompareTag( TagLiteral.Ground ) )
        {
            _animator.SetBool( AnimLiteral.IsFall, false );

            _isGround = true;
        }

        if ( collision.impulse.magnitude > _topplingForce && !collision.gameObject.CompareTag( TagLiteral.Ground ) )
        {
            _animator.SetBool( AnimLiteral.IsFall, true );

            PlayFallSound();

            CheckFallStateAfterDelay( delay: 0.5f ).Forget();
        }
    }

    private void OnCollisionExit( Collision other )
    {
        if ( !photonView.IsMine )
            return;

        if ( other.gameObject.CompareTag( TagLiteral.Ground ) )
        {
            _isGround = false;
        }
    }

    // Ground에 닿았는데도 여전히 Fall상태일때 IsFall을 false로 만들어주는 함수.
    private async UniTaskVoid CheckFallStateAfterDelay( float delay )
    {
        await UniTask.Delay( TimeSpan.FromSeconds( delay ) );

        if ( _animator.GetBool( AnimLiteral.IsFall ) && _isGround )
        {
            _animator.SetBool( AnimLiteral.IsFall, false );
        }
    }


    private void OnTriggerEnter( Collider other )
    {
        if ( !photonView.IsMine )
            return;

        if ( other.gameObject.CompareTag( TagLiteral.Boundary ) )
        {
            _animator.SetBool( AnimLiteral.IsFall, true );
        }

        if ( other.gameObject.CompareTag( TagLiteral.Respawn ) )
        {
            _animator.SetBool( AnimLiteral.IsRespawning, true );
        }

    }

    private void ReleaseGrab()
    {
        _animator.SetBool( AnimLiteral.IsGrab, false );
        _animator.SetBool( AnimLiteral.IsGrabSuccess, false );
    }

    #region AnimationEvent

    private int _randomAudioClipIndex;
    public void WalkFootStep()
    {
        photonView.RPC("RpcWalkFootStep", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RpcWalkFootStep()
    {
        _randomAudioClipIndex = Random.Range(0, AudioRegistry.WalkFootStepSFX.Length);
        _audioSource.PlayOneShot(AudioRegistry.WalkFootStepSFX[_randomAudioClipIndex]);
    }

    public void RunFootStep()
    {
        photonView.RPC("RpcRunFootStep", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RpcRunFootStep()
    {
        _randomAudioClipIndex = Random.Range(0, AudioRegistry.RunFootStepSFX.Length);
        _audioSource.PlayOneShot(AudioRegistry.RunFootStepSFX[_randomAudioClipIndex]);
    }

    public void PlayJumpSound()
    {
        photonView.RPC("RpcPlayJumpSound", RpcTarget.AllBuffered );
    }

    [PunRPC]
    public void RpcPlayJumpSound()
    {
        _audioSource.PlayOneShot( AudioRegistry.JumpSFX );
    }

    public void PlayDiveSound()
    {
        photonView.RPC("RpcPlayDiveSound", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RpcPlayDiveSound()
    {
        _audioSource.PlayOneShot(AudioRegistry.DiveSFX);
    }

    public void PlayRespawnSound()
    {
        photonView.RPC("RpcPlayRespawnSound", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RpcPlayRespawnSound()
    {
        _audioSource.PlayOneShot(AudioRegistry.RespawnSFX);
    }

    #endregion

    private void PlayFallSound()
    {
        photonView.RPC("RpcPlayFallSound", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RpcPlayFallSound()
    {
        _audioSource.PlayOneShot(AudioRegistry.FallSFX);
    }
}