using UnityEngine;

public class FruitChuteReleaseZone : MonoBehaviour
{
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Fruit"))
        {
            Fruit fruit = col.gameObject.GetComponent<Fruit>();
            fruit.Release();
        }
    }
}
