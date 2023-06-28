using Photon.Pun;
using UnityEngine;

public class PlayerPhotonController : MonoBehaviourPunCallbacks
{
    public SkinnedMeshRenderer BodyMeshRenderer;

    [PunRPC]
    public void SetTextureIndex(int index)
    {
        BodyMeshRenderer.material.mainTexture = PlayerTextureRegistry.PlayerTextures[index];
    }
}

