using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviourPun
{
    public Vector3 InputVec { get; private set; }
    public Vector2 ScreenToMousePos { get; private set; }
    
    public event Action OnMovement;
    
    private void Update()
    {
        if (_isMoving && photonView.IsMine)
        {
            OnMovement?.Invoke();
        }
    }

    public bool CannotMove { get; set; }
    private bool _isMoving;
    
    public void OnMove(InputAction.CallbackContext context)
    {
        InputVec = context.ReadValue<Vector3>();
        
        if (context.started && photonView.IsMine)
        {
            _isMoving = true;
        }

        if (context.canceled && photonView.IsMine)
        {
            _isMoving = false;
        }
    }

    public bool IsJump { get; private set; }
    public void OnJump(InputAction.CallbackContext context)
    {
        // Jump Button을 눌렀을때 JumpState를 실행한다.
        if (context.started && photonView.IsMine)
        {
            IsJump = true;
        }

        if (context.canceled && photonView.IsMine)
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
        if (context.started && photonView.IsMine)
        {
            IsAttemptingGrab = true;
        }

        if (context.canceled && photonView.IsMine)
        {
            IsAttemptingGrab = false;
        }
    }
    
    public bool IsDive { get; private set; }
    public void OnDive(InputAction.CallbackContext context)
    {
        if (context.started && photonView.IsMine)
        {
            IsDive = true;
        }

        if (context.canceled && photonView.IsMine)
        {
            IsDive = false;
        }
    }

    public event Action OnMouseMove;
    public void OnMouse(InputAction.CallbackContext context)
    {
        if (photonView.IsMine)
        {
            ScreenToMousePos = context.ReadValue<Vector2>();
            OnMouseMove?.Invoke();            
        }
    }
}
