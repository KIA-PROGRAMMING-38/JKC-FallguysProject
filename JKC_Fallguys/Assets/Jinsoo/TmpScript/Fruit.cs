using UnityEngine;
    
/// <summary>
/// FruitPrefabRegistry에 과일들을 저장하기 위한 클래스.
/// </summary>
public class Fruit : MonoBehaviour
{
    // 이 Fruit이 속한 풀의 소유자입니다.
    public FruitPool PoolOwner { private get; set; }
    
    public void SetActive(bool activeState)
    {
        gameObject.SetActive(activeState);
    }

    public void Release()
    {
        PoolOwner.FruitPoolInstance.Release(this);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
