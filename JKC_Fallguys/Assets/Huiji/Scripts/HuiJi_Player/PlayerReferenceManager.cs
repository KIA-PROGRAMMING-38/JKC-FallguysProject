using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReferenceManager : MonoBehaviour
{
    private PlayerInput _playerInput;
    private CameraAngle _cameraAngle;
    private PlayerPhysicsController _playerPhysicsController;
    private Transform _playerCharacter;

    private void Awake()
    {
        _playerInput = GetComponentInChildren<PlayerInput>();
        _cameraAngle = GetComponentInChildren<CameraAngle>();
        _playerPhysicsController = GetComponentInChildren<PlayerPhysicsController>();
        _playerCharacter = transform.Find("Character");
    }

    private void Start()
    {
        _cameraAngle.BindPlayerData(_playerInput, _playerCharacter);
        _playerPhysicsController.BindCameraAngle(_cameraAngle);
    }
}
