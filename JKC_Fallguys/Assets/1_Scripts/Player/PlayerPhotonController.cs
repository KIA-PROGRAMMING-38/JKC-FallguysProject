using Photon.Pun;
using UnityEngine;

public class PlayerPhotonController : MonoBehaviourPunCallbacks
{
    public SkinnedMeshRenderer BodyMeshRenderer;

    [PunRPC]
    public void RpcSetInitialize(int actorNum, string nickName, int textureIndex)
    {
        transform.parent.SetParent(StageManager.Instance.PlayerRepository.gameObject.transform);
        
        PlayerReferenceManager refManager = transform.parent.GetComponent<PlayerReferenceManager>();
        refManager.SetPlayerInformation(actorNum, nickName);
        BodyMeshRenderer.material.mainTexture = PlayerTextureRegistry.PlayerTextures[textureIndex];
    }

    [PunRPC]
    public void RpcSetDeActivePlayerObject()
    {
        transform.parent.gameObject.SetActive(false);
    }
}