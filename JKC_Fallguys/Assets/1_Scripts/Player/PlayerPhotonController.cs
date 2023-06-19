using Photon.Pun;
using UnityEngine;

public class PlayerPhotonController : MonoBehaviourPun
{
    private PlayerReferenceManager _referenceManager;

    [SerializeField]
    public SkinnedMeshRenderer _bodyMeshRenderer;

    public void OnInitialize(PlayerReferenceManager referenceManager)
    {
        _referenceManager = referenceManager;
    }
    
    [PunRPC]
    public void SendMessageWinTheStage()
    {
        _referenceManager.PhotonStageSceneRoomManager.CompleteStageAndRankPlayers();
    }
    
    [PunRPC]
    public void AddPlayerToRankingOnGoal(int playerIndex)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StageDataManager.Instance.AddPlayerToRanking(playerIndex);
        }
    }
}
