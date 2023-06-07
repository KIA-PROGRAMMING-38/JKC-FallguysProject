using UnityEngine;

public abstract class SceneInitialize : MonoBehaviour
{
    protected void Start()
    {
        InitializeScene();
    }

    /// <summary>
    /// SceneInitialize를 상속받는 클래스에서 호출될 함입니다.
    /// </summary>
    protected void InitializeScene()
    {
        OnGetResources();
    }

    /// <summary>
    /// Resoureces를 가져오는 함수입니다.
    /// 데이터를 가져와서 Instantiate를 진행합니다.
    /// </summary>
    protected abstract void OnGetResources();
}