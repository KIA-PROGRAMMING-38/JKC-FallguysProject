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
    }

    /// <summary>
    /// Resoureces를 가져오는 함수입니다.
    /// 데이터를 가져와서 Instantiate를 진행합니다.
    /// </summary>
    protected abstract void OnGetResources();

    /// <summary>
    /// 해당 씬으로 돌아갈 경우, Model의 값을 초기화를 담당하는 함수입니다.
    /// </summary>
    protected virtual void InitializeData()
    {
        
    }
}