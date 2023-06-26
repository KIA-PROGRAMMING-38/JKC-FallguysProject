using LiteralRepository;
using UnityEngine;

public class FruitChuteReleaseZone : MonoBehaviour
{
    private Vector3 _fallGuyRespawnPos = new Vector3(0f, 0f, 4f);
    private Quaternion _fallGuyRespawnEuler = Quaternion.identity;
    
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag(TagLiteral.Fruit))
        {
            Fruit fruit = col.gameObject.GetComponent<Fruit>();
            
            fruit.ReleaseToPool();
        }

        if (col.gameObject.CompareTag(TagLiteral.Player))
        {
            PlayerPhysicsController physicsController = col.gameObject.GetComponent<PlayerPhysicsController>();
            physicsController.Respawn(_fallGuyRespawnPos, _fallGuyRespawnEuler);
        }
    }
}
