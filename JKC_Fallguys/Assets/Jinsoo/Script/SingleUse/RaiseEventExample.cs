using System;
using ExitGames.Client.Photon;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Random = UnityEngine.Random;

public class RaiseEventExample : MonoBehaviourPun
{
    private SpriteRenderer _spriteRenderer;

    private const byte COLOR_CHANGE_EVENT = 0;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // 로컬 플레이어가 조작하고 있고, 스페이스 바를 누를 경우
        if (photonView.IsMine && Input.GetKey(KeyCode.Space))
        {
            ChangeColor();
        }
    }

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }
    
    /// <summary>
    /// Photon 네트워크애서 이벤트를 받을 때 호출된다
    /// EventData 객체로부터 CustomData를 읽어 색상 정보를 얻어 _spriteRenderer의 Color 변경
    /// </summary>
    /// <param name="obj"></param>
    private void NetworkingClient_EventReceived(EventData obj)
    {
        if (obj.Code == COLOR_CHANGE_EVENT)
        {
            object[] datas = (object[])obj.CustomData;
            float r = (float)datas[0];
            float g = (float)datas[1];
            float b = (float)datas[2];
            _spriteRenderer.color = new Color(r, g, b, 1f);
        }
    }

    /// <summary>
    /// 랜덤한 색상 값을 생성하고 _sprite의 색상을 변경
    /// </summary>
    private void ChangeColor()
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);

        _spriteRenderer.color = new Color(r, g, b, 1f);

        // 색상 값들을 포함하는 object[] 생성
        object[] datas = new object[] { r, g, b };

        // RaiseEvent는 사용자 정의 이벤트를 네트워크를 통해 보낼 수 있게 해준다
        // 첫 번째 인자는 eventCode를 의미한다. byte값으로 표현되며 이벤트를 식별하는데 사용된다
        // 두 번째 인자는 이벤트와 함께 전송될 사용자 정의 데이터를 나타낸다
        // RaiseEventOptions는 이벤트 전송 옵션을 의미한다. 어떤 대상에게 보낼지, 어떻게 보낼 것인지 지정한다
        // SendOptions는 이벤트를 어떻게 보낼지 지정한다. SendUnreliable은 신뢰성이 떨어지지만 빠른 방식으로 보내겠다는 의미이다
        // 이 메서드를 통해 보낸 이벤트는 PhotonNetwork.NetworkingClient.EventReceived 이벤트 핸들러에서 받을 수 있다
        PhotonNetwork.RaiseEvent
            (COLOR_CHANGE_EVENT, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
    }
}
