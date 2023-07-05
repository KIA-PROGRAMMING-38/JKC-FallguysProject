using LiteralRepository;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonStageSceneEventManager : MonoBehaviourPunCallbacks
{
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(SceneIndex.Lobby);
    }
    
    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);

        Debug.Log("호출");
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}
