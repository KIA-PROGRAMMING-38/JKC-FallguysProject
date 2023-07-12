using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Photon.Pun;

public class StageState_RoundCompletion : StageState
{
    public override void AddToDictionary()
    {
        StageManager.Instance.StageDataManager.SequenceActionDictionary[StageDataManager.StageSequence.RoundCompletion] = this;
    }

    public override void Action()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        AsyncEndProductionScheduler().Forget();
    }
    
    private async UniTaskVoid AsyncEndProductionScheduler()
    {
        BroadCastData();
        
        await UniTask.Delay(TimeSpan.FromSeconds(5f), DelayType.UnscaledDeltaTime);
        
        photonView.RPC("RpcSetTransitioningState", RpcTarget.All);
    }
    
    private void BroadCastData()
    {
        string playerScoresByIndexJson =
            JsonConvert.SerializeObject(StageManager.Instance.PlayerContainer.PlayerDataByIndex);

        photonView.RPC("RpcUpdateStageDataOnAllClients", RpcTarget.AllBuffered,
            playerScoresByIndexJson,
            StageManager.Instance.PlayerContainer.CachedPlayerIndicesForResults.ToArray());
    }

    [PunRPC]
    public void RpcUpdateStageDataOnAllClients(string playerScoresByIndexJson, int[] playerRanking)
    {
        StageManager.Instance.PlayerContainer.PlayerDataByIndex =
            JsonConvert.DeserializeObject<Dictionary<int, PlayerData>>(playerScoresByIndexJson);
        StageManager.Instance.PlayerContainer.CachedPlayerIndicesForResults = playerRanking.ToList();
    }
    
    [PunRPC]
    public void RpcSetTransitioningState()
    {
        StageManager.Instance.StageDataManager.SetSequence(StageDataManager.StageSequence.Transitioning);
    }
}
