using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Photon.Pun;
using UniRx;
using UnityEngine;

/// <summary>
/// 점수를 결산한 뒤 다음 씬을 실행시키는 클래스입니다.
/// </summary>
public class PhotonStageSceneRoomManager : MonoBehaviourPun
{
    private readonly CancellationTokenSource _cts = new CancellationTokenSource();

    private void Awake()
    {
        AddComponentStageStates();
    }

    private void Start()
    {
        Initialize();
        
        StageDontDestroyOnLoadSet();
    }

    private void AddComponentStageStates()
    {
        Type parentType = typeof(StageState);
        Assembly assembly = Assembly.GetExecutingAssembly();
        IEnumerable<Type> types = assembly.GetTypes().Where(t => t.IsSubclassOf(parentType));

        foreach (Type type in types)
        {
            gameObject.AddComponent(type);
        }
    }

    private void Initialize()
    {
        StageManager.Instance.StageDataManager.CurrentSequence
            .DistinctUntilChanged()
            .Subscribe(sequence => StageManager.Instance.StageDataManager.SequenceActionDictionary[sequence].Action())
            .AddTo(this);
    }
    
    private void StageDontDestroyOnLoadSet()
    {
        photonView.RPC("RpcSetParentStageRepository", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RpcSetParentStageRepository()
    {
        transform.SetParent(StageManager.Instance.ObjectRepository.transform);
    }
}
