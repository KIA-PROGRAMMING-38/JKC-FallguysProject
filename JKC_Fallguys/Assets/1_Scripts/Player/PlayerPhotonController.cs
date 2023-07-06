using Photon.Pun;
using UnityEngine;

public class PlayerPhotonController : MonoBehaviourPunCallbacks
{
    public SkinnedMeshRenderer BodyMeshRenderer;

    [PunRPC]
    public void RpcSetTextureIndex(int index)
    {
        BodyMeshRenderer.material.mainTexture = PlayerTextureRegistry.PlayerTextures[index];
    }

    [PunRPC]
    public void RpcSetInitialize(int actorNum, string nickName)
    {
        transform.parent.SetParent(StageDataManager.Instance.gameObject.transform);
        
        PlayerReferenceManager refManager = transform.parent.GetComponent<PlayerReferenceManager>();
        refManager.SetPlayerInformation(actorNum, nickName);
    }

    [PunRPC]
    public void RpcSetDeActivePlayerObject()
    {
        transform.parent.gameObject.SetActive(false);
    }
}