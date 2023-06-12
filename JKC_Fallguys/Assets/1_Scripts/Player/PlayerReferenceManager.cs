using UnityEngine;

public class PlayerReferenceManager : MonoBehaviour
{
    private PlayerInput _playerInput;
    private CameraAngle _cameraAngle;
    private PlayerPhysicsController _playerPhysicsController;
    private Transform _playerCharacter;
    private PlayerPhotonController _photonController;

    public PhotonStageSceneRoomManager PhotonStageSceneRoomManager { get; private set; }

    private void Awake()
    {
        _playerInput = GetComponentInChildren<PlayerInput>();
        _cameraAngle = GetComponentInChildren<CameraAngle>();
        _playerPhysicsController = GetComponentInChildren<PlayerPhysicsController>();
        _playerCharacter = transform.Find("Character");
        _photonController = _playerCharacter.GetComponent<PlayerPhotonController>();
    }

    private void Start()
    {
        _cameraAngle.BindPlayerData(_playerInput, _playerCharacter);
        _playerPhysicsController.BindCameraAngle(_cameraAngle);
        _photonController.OnInitialize(this);
    }

    public void OnInitialize(PhotonStageSceneRoomManager photonStageSceneRoomManager)
    {
        PhotonStageSceneRoomManager = photonStageSceneRoomManager;
    }
}
