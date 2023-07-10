using Photon.Pun;
using UnityEngine;

public abstract class SceneInitializer : MonoBehaviour
{
    protected virtual void Awake()
    {
        InitializeData();
    }

    protected virtual void Start()
    {
        OnGetResources();
        SetAudio();

        if (PhotonNetwork.IsMasterClient)
        {
            OnGetResourcesOnMasterClient();
        }
    }

    /// <summary>
    /// Resoureces를 가져오는 함수입니다.
    /// 데이터를 가져와서 Instantiate를 진행합니다.
    /// </summary>
    protected abstract void OnGetResources();

    /// <summary>
    /// 마스터 클라이언트에서만 가져올 Resources를 정의하는 함수입니다.
    /// </summary>
    protected virtual void OnGetResourcesOnMasterClient()
    {
        
    }

    /// <summary>
    /// 해당 씬으로 돌아갈 경우, Data 초기화를 담당하는 함수입니다.
    /// </summary>
    protected virtual void InitializeData()
    {
        
    }

    /// <summary>
    /// 해당 씬에 진입할 경우, 플레이할 음악을 세팅하는 함수입니다.
    /// </summary>
    protected virtual void SetAudio()
    {
        
    }
}