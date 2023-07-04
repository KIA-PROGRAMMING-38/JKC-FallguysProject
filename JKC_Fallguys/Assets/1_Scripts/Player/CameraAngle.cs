using Photon.Pun;
using UnityEngine;

public class CameraAngle : MonoBehaviourPun
{
    private PlayerInput _playerInput;
    private Transform _playerChracter;

    private void Awake()
    {
        Camera camera = GetComponentInChildren<Camera>();
        AudioListener myAudioListener = camera.GetComponent<AudioListener>();

        if (photonView.IsMine)
        {
            camera.enabled = true;
            myAudioListener.enabled = true;
        }
        else
        {
            camera.enabled = false;
            myAudioListener.enabled = false;
        }
    }

    public void BindPlayerData(PlayerInput playerInput, Transform playerCharacter)
    {
        _playerInput = playerInput;
        _playerChracter = playerCharacter;
        _playerInput.OnMouseMove -= SetCameraAngle;
        _playerInput.OnMouseMove += SetCameraAngle;
    }

    // 3인칭 카메라 앵글 구현
    private void SetCameraAngle()
    {
        Vector3 cameraAngle = transform.rotation.eulerAngles;
        // 3인칭 기준으로 위 아래 시야를 rotation해주기 위해서는 x축을 기준으로 회전해야함
        float xValueOfAngle = cameraAngle.x - _playerInput.ScreenToMousePos.y;

        // 원작에서도 카메라의 시점을 위쪽으로 돌릴 수 있는 한계가 존재, 이를 구현
        if (xValueOfAngle < 180f)
        {
            xValueOfAngle = Mathf.Clamp(xValueOfAngle, -1f, 50f);
        }
        // 원작에서도 카메라의 시점을 아래쪽으로 돌릴 수 있는 한계가 존재, 이를 구현
        else
        {
            xValueOfAngle = Mathf.Clamp(xValueOfAngle, 345f, 361f);
        }

        transform.rotation = Quaternion.Euler(
            xValueOfAngle,
            cameraAngle.y + _playerInput.ScreenToMousePos.x,
            cameraAngle.z);
    }

    private void LateUpdate()
    {
        if (!photonView.IsMine)
            return;
        
        FollowPlayerBody();
    }

    // 카메라가 플레이어의 좌표를 따라가는 함수
    private void FollowPlayerBody()
    {
        Vector3 targetPos = new Vector3
            (_playerChracter.position.x, _playerChracter.position.y - 1, _playerChracter.position.z);
        transform.position = targetPos;
    }
}