using LiteralRepository;
using UnityEngine;

public class FruitChuteReleaseZone : MonoBehaviour
{
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag(TagLiteral.Fruit))
        {
            Fruit fruit = col.gameObject.GetComponent<Fruit>();
            
            fruit.ReleaseToPool();
        }
    }
}
