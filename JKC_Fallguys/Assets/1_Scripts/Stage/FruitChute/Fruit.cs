using Photon.Pun;
using UnityEngine;
    
/// <summary>
/// FruitPrefabRegistry에 과일들을 저장하기 위한 클래스.
/// </summary>
public class Fruit : MonoBehaviour
{
    public DefaultPool DefaultPool { private get; set; }

    public void ReleaseToPool()
    {
        DefaultPool.Destroy(gameObject);
    }
}