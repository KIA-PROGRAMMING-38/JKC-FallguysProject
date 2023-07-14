using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviourPun
{
    public Vector3 InputVec { get; private set; }
    public Vector2 ScreenToMousePos { get; private set; }
    public bool IsNothingUnderfoot { get; set; }

    public event Action OnMovement;
    
    private void Update()
    {
        if (!photonView.IsMine || StageManager.Instance.ObjectRepository.CurrentSequence.Value != ObjectRepository.StageSequence.GameInProgress)
            return;
        
        if (_isMoving)
        {
            OnMovement?.Invoke();
        }
    }

    public bool CannotMove { get; set; }
    private bool _isMoving;
    
    private Vector3 _lastFrameInputVec;

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started || context.performed) // 키를 누르고 있는 동안 호출
        {
            _isMoving = true;
            InputVec = context.ReadValue<Vector3>();
        }

        if (context.canceled)
        {
            _isMoving = false;
        }
    }

    public bool IsJump { get; private set; }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!photonView.IsMine || StageManager.Instance.ObjectRepository.CurrentSequence.Value != ObjectRepository.StageSequence.GameInProgress)
            return;
        
        // Jump Button을 눌렀을때 JumpState를 실행한다.
        if (context.started)
        {
            IsJump = true;
        }

        if (context.canceled)
        {
            IsJump = false;
        }
    }

    public event Action OnReleaseGrab;

    private bool _isAttemptingGrab;
    public bool IsAttemptingGrab
    {
        get => _isAttemptingGrab;
        
        set
        {
            _isAttemptingGrab = value;

            if (value == false)
            {
                OnReleaseGrab?.Invoke();
            }
        }
    }

    public void OnGrab(InputAction.CallbackContext context)
    {
        if (!photonView.IsMine || StageManager.Instance.ObjectRepository.CurrentSequence.Value != ObjectRepository.StageSequence.GameInProgress)
            return;
        
        if (context.started)
        {
            IsAttemptingGrab = true;
        }

        if (context.canceled)
        {
            IsAttemptingGrab = false;
        }
    }
    
    public bool IsDive { get; private set; }
    public void OnDive(InputAction.CallbackContext context)
    {
        if (!photonView.IsMine || StageManager.Instance.ObjectRepository.CurrentSequence.Value != ObjectRepository.StageSequence.GameInProgress)
            return;
        
        if (context.started)
        {
            IsDive = true;
        }

        if (context.canceled)
        {
            IsDive = false;
        }
    }

    public event Action OnMouseMove;
    [SerializeField] private float _mouseSensitivity;
    public void OnMouse(InputAction.CallbackContext context)
    {
        if (photonView.IsMine)
        {
            ScreenToMousePos = context.ReadValue<Vector2>() * _mouseSensitivity;
            OnMouseMove?.Invoke();            
        }
    }
}
