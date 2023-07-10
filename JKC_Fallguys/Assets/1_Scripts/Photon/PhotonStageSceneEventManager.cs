using LiteralRepository;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonStageSceneEventManager : MonoBehaviourPunCallbacks
{
    public override void OnLeftRoom()
    {
        SceneChangeHelper.ChangeLocalScene(SceneIndex.Lobby);
    }
    
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"Disconnected Issue : {cause}");
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}
