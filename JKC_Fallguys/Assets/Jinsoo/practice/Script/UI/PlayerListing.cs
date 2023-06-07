using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

/// <summary>
/// 게임 플레이어의 정보를 화면에 표시하는데 사용되는 컴포넌트
/// </summary>
public class PlayerListing : MonoBehaviourPunCallbacks 
{
    [SerializeField] private Text _text;

    // Photon.Realtime의 속성
    // 플레이어의 정보 저장
    public Player Player { get; private set; }
    public bool Ready = false;
    
    /// <summary>
    /// 플레이어 타입의 인자를 받아 세팅하고, 플레이어의 닉네임 또한 세팅
    /// </summary>
    /// <param name="player"></param>
    public void SetPlayerInfo(Player player)
    {
        Player = player;

        SetPlayerText(player);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);

        if (targetPlayer != null && targetPlayer == Player)
        {
            if (changedProps.ContainsKey("RandomNumber"))
            {
                SetPlayerText(targetPlayer);
            }
        }
    }

    private void SetPlayerText(Player player)
    {
        int result = -1;
        if (player.CustomProperties.ContainsKey("RandomNumber"))
        {
            result = (int)player.CustomProperties["RandomNumber"];
        }
        
        _text.text = result.ToString() + ", " + player.NickName;
    }
}
