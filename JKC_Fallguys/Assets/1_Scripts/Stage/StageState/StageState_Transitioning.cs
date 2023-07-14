using LiteralRepository;
using Photon.Pun;
using Util.Helper;

public class StageState_Transitioning : StageState
{
    protected override void AddToSequenceActionDictionary()
    {
        StageManager.Instance.StageDataManager.SequenceActionDictionary[StageDataManager.StageSequence.Transitioning] = this;
    }

    public override void Action()
    {
        StageClear();

        if (!PhotonNetwork.IsMasterClient)
            return;

        if (StageManager.Instance.StageDataManager.IsFinalRound())
        {
            SceneChangeHelper.ChangeNetworkLevel(SceneIndex.GameResult);
        }
        else if (!StageManager.Instance.StageDataManager.IsFinalRound())
        {
            SceneChangeHelper.ChangeNetworkLevel(SceneIndex.RoundResult);
        }
    }
    
    private void StageClear()
    {
        GameObjectHelper.DestroyAllChildren(StageManager.Instance.PlayerRepository.gameObject);
        GameObjectHelper.DestroyAllChildren(StageManager.Instance.ObjectRepository.gameObject);
    }
}
