using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Photon.Pun;
using Util.Helper;

public class StageState_GameCompletion : StageState
{
    protected override void AddToSequenceActionDictionary()
    {
        StageManager.Instance.ObjectRepository.SequenceActionDictionary[ObjectRepository.StageSequence.GameCompletion] = this;
    }

    public override void Action()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        AsyncEndProductionScheduler().Forget();
    }

    private async UniTaskVoid AsyncEndProductionScheduler()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(2f), DelayType.UnscaledDeltaTime);

        ScoreCalculateHelper.Calculate();
        UpdatePlayerRanking();

        await UniTask.Delay(TimeSpan.FromSeconds(1f), DelayType.UnscaledDeltaTime);

        photonView.RPC("RpcSetRoundState", RpcTarget.All);
    }

    private void UpdatePlayerRanking()
    {
        List<KeyValuePair<int, PlayerData>> sortedPlayers =
            StageManager.Instance.PlayerContainer.PlayerDataByIndex.OrderByDescending(pair => pair.Value.Score)
                .ToList();

        StageManager.Instance.PlayerContainer.CachedPlayerIndicesForResults.Clear();

        foreach (KeyValuePair<int, PlayerData> pair in sortedPlayers)
        {
            StageManager.Instance.PlayerContainer.CachedPlayerIndicesForResults.Add(pair.Key);
        }
    }

    [PunRPC]
    public void RpcSetRoundState()
    {
        StageManager.Instance.ObjectRepository.SetSequence(ObjectRepository.StageSequence.RoundCompletion);
    }
}
