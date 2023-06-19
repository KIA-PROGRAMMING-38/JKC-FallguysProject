using Photon.Pun;

public class PlayerPhotonController : MonoBehaviourPun
{
    private PlayerReferenceManager _referenceManager;

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
