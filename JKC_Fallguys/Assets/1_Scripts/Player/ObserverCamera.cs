using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObserverCamera : MonoBehaviour
{
    private Transform _followPlayerCharacter;
    private Vector2 _observingMouseVec;

    private PlayerObserverCamera _playerObserverCamera;

    public void Initialize(PlayerObserverCamera playerObserverCamera)
    {
        _playerObserverCamera = playerObserverCamera;
    }

    private void OnEnable()
    {
        _playerObserverCamera.ObservedNextPlayer();
    }

    public void OnUpdateMousePos(InputAction.CallbackContext context)
    {
        _observingMouseVec = context.ReadValue<Vector2>();
        
        SetCameraAngle();
    }

    public void OnUpdateNextTarget(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _playerObserverCamera.ObservedNextPlayer();
        }
    }

    public void OnUpdatePrevTarget(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _playerObserverCamera.ObservedPrevPlayer();
        }
    }
    
    private void SetCameraAngle()
    {
        Vector3 cameraAngle = transform.rotation.eulerAngles;
        
        float xValueOfAngle = cameraAngle.x - _observingMouseVec.y;

        if (xValueOfAngle < 180f)
        {
            xValueOfAngle = Mathf.Clamp(xValueOfAngle, -1f, 50f);
        }
        else
        {
            xValueOfAngle = Mathf.Clamp(xValueOfAngle, 345f, 361f);
        }

        transform.rotation = Quaternion.Euler(
            xValueOfAngle,
            cameraAngle.y + _observingMouseVec.x,
            cameraAngle.z);
    }
    
    public void UpdatePlayerTarget(Transform followPlayerCharacter)
    {
        _followPlayerCharacter = followPlayerCharacter;
    }
    
    private void LateUpdate()
    {
        FollowPlayerBody();
    }
    
    private void FollowPlayerBody()
    {
        Vector3 targetPos = new Vector3
            (_followPlayerCharacter.position.x, _followPlayerCharacter.position.y - 1, _followPlayerCharacter.position.z);
        transform.position = targetPos;
    }
}
