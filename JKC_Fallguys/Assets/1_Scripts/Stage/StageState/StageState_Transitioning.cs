using LiteralRepository;
using Photon.Pun;
using Util.Helper;

public class StageState_Transitioning : StageState
{
    protected override void AddToSequenceActionDictionary()
    {
        StageManager.Instance.ObjectRepository.SequenceActionDictionary[ObjectRepository.StageSequence.Transitioning] = this;
    }

    public override void Action()
    {
        StageClear();

        if (!PhotonNetwork.IsMasterClient)
            return;

        if (StageManager.Instance.ObjectRepository.IsFinalRound())
        {
            SceneChangeHelper.ChangeNetworkLevel(SceneIndex.GameResult);
        }
        else if (!StageManager.Instance.ObjectRepository.IsFinalRound())
        {
            SceneChangeHelper.ChangeNetworkLevel(SceneIndex.RoundResult);
        }
    }
    
    private void StageClear()
    {
        StageManager.Instance.PlayerRepository.Clear();
        StageManager.Instance.ObjectRepository.Clear();
    }
}
