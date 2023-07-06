using LiteralRepository;
using UnityEngine;

public class ObstacleBumper : MonoBehaviour
{
    public float reflectForce;
    
    private const float MIN_RFLECTION_VALUE = 50f;
    private const float MAX_RFLECTION_VALUE = 270f;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag(TagLiteral.Player))
        {
            Debug.Log(col.gameObject.name);
            
            Rigidbody collisionObjectRigid = col.gameObject.GetComponent<Rigidbody>();
            Vector3 collisionVector = collisionObjectRigid.velocity.normalized;
            Vector3 normalVector = col.contacts[0].normal.normalized;
            Vector3 reflectionDirection = 
                Vector3.Reflect(collisionVector,normalVector);

            float collisionImpulseForce = col.impulse.magnitude;
            collisionImpulseForce = Mathf.Clamp(collisionImpulseForce, MIN_RFLECTION_VALUE, MAX_RFLECTION_VALUE);
        
            collisionObjectRigid.velocity = reflectionDirection * reflectForce * collisionImpulseForce;            
        }
    }
}
