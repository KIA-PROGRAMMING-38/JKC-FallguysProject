using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerPhotonController : MonoBehaviourPunCallbacks, IPunObservable
{
    private PlayerReferenceManager _referenceManager;
    public List<Texture2D> PlayerTextures = new List<Texture2D>();

    [SerializeField]
    public SkinnedMeshRenderer _bodyMeshRenderer;

    public new PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        // Load textures
        PlayerTextures = new List<Texture2D>(Resources.LoadAll<Texture2D>("Textures/PlayerTexture"));
    }

    public void OnInitialize(PlayerReferenceManager referenceManager)
    {
        _referenceManager = referenceManager;
    }

    
    private int _textureIndex = 0;

    private void Update()
    {
        if (!photonView.IsMine) 
            return; // Local player only

        if (Input.GetKeyDown(KeyCode.N))
        {
            if (_textureIndex < PlayerTextures.Count - 1)
            {
                _textureIndex++;
            }
            else
            {
                _textureIndex = 0;
            }

            Debug.Log("Texture index changed to: " + _textureIndex);  // Add this line

            photonView.RPC("SetTextureIndex", RpcTarget.AllBuffered, _textureIndex);
        }
    }

    [PunRPC]
    public void SetTextureIndex(int index)
    {
        if (PlayerTextures == null || PlayerTextures.Count == 0 || index >= PlayerTextures.Count)
            return;

        _textureIndex = index;
        _bodyMeshRenderer.material.mainTexture = PlayerTextures[index];
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_textureIndex);
        }
        else
        {
            _textureIndex = (int)stream.ReceiveNext();
            SetTextureIndex(_textureIndex);
        }
    }
}
