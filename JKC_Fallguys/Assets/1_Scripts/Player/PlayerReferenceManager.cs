using LiteralRepository;
using Photon.Pun;
using UnityEngine;

public class PlayerReferenceManager : MonoBehaviourPun
{
    private PlayerInput _playerInput;
    private CameraAngle _cameraAngle;
    private PlayerPhysicsController _playerPhysicsController;
    private Transform _playerCharacter;

    public int ArchievePlayerActorNumber { get; private set;}
    public string ArchievePlayerNickName { get; private set; }

    private void Awake()
    {
        if (!photonView.IsMine)
            return;

        _playerInput = GetComponentInChildren<PlayerInput>();
        _cameraAngle = GetComponentInChildren<CameraAngle>();
        _playerPhysicsController = GetComponentInChildren<PlayerPhysicsController>();
        _playerCharacter = transform.Find("Character");
    }

    public void SetPlayerInformation(int actorNum, string nickName)
    {
        ArchievePlayerActorNumber = actorNum;
        ArchievePlayerNickName = nickName;
    }
    
    private void Start()
    {
        if (!photonView.IsMine)
            return;
        
        _cameraAngle.BindPlayerData(_playerInput, _playerCharacter);
        _playerPhysicsController.BindCameraAngle(_cameraAngle);
    }
}
