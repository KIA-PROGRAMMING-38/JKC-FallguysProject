using LiteralRepository;
using Photon.Pun;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float speed;
    private Rigidbody _conveyorBeltRigidbody;
    void Awake()
    {
        _conveyorBeltRigidbody= GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 position = _conveyorBeltRigidbody.position;
        _conveyorBeltRigidbody.position += Vector3.forward * speed * Time.deltaTime;
        _conveyorBeltRigidbody.MovePosition(position);
    }

    [SerializeField] private float _conveyorForce;

    private void OnCollisionStay(Collision collision)
    {
        if ( collision.gameObject.CompareTag(TagLiteral.Player) )
        {
            PhotonView playerPhotonView = collision.gameObject.GetComponent<PhotonView>();
            if ( playerPhotonView != null && playerPhotonView.IsMine )
            {
                PlayerPhysicsController playerPhysicsController = collision.gameObject.GetComponent<PlayerPhysicsController>();
                Debug.Assert( playerPhysicsController != null );
                playerPhysicsController.AddForcePlayerMove(-transform.right * _conveyorForce);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if ( collision.gameObject.CompareTag(TagLiteral.Player) )
        {
            PhotonView playerPhotonView = collision.gameObject.GetComponent<PhotonView>();
            if ( playerPhotonView != null && playerPhotonView.IsMine )
            {
                PlayerPhysicsController playerPhysicsController = collision.gameObject.GetComponent<PlayerPhysicsController>();
                Debug.Assert(playerPhysicsController != null);
                playerPhysicsController.ClearPlayerMove();
            }
        }
    }
}
