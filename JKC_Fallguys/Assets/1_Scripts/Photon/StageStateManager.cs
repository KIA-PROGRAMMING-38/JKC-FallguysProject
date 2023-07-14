using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Photon.Pun;
using UniRx;

public class StageStateManager : MonoBehaviourPun
{
    private void Awake()
    {
        AddComponentStageStates();
    }

    private void Start()
    {
        Initialize();
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
        photonView.RPC("RpcSetParentStageRepository", RpcTarget.AllBuffered);
        
        StageManager.Instance.ObjectRepository.CurrentSequence
            .DistinctUntilChanged()
            .Subscribe(sequence => StageManager.Instance.ObjectRepository.SequenceActionDictionary[sequence].Action())
            .AddTo(this);
    }
    
    [PunRPC]
    public void RpcSetParentStageRepository()
    {
        transform.SetParent(StageManager.Instance.ObjectRepository.transform);
    }
}
