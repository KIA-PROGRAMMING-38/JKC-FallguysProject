using System;
using Cysharp.Threading.Tasks;
using LiteralRepository;
using TMPro;
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
            Rigidbody collisionObjectRigid = col.gameObject.GetComponent<Rigidbody>();
            Vector3 collisionVector = collisionObjectRigid.velocity.normalized;
            Vector3 normalVector = col.contacts[0].normal.normalized;
            Vector3 reflectionDirection = 
                Vector3.Reflect(collisionVector,normalVector);

            float collisionImpulseForce = col.impulse.magnitude;
            collisionImpulseForce = Mathf.Clamp(collisionImpulseForce, MIN_RFLECTION_VALUE, MAX_RFLECTION_VALUE);
        
            UnableToControlPlayerInput(col).Forget();

            collisionObjectRigid.velocity = reflectionDirection * reflectForce * collisionImpulseForce;            
        }
    }
    
    // 플레이어가 부딪힐 경우 잠시 조작할 수 없게 만드는 UniTask
    private async UniTaskVoid UnableToControlPlayerInput(Collision col)
    {
        PlayerInput playerInput = col.gameObject.GetComponentInParent<PlayerInput>();
        playerInput.CannotMove = true;
        
        await UniTask.Delay(TimeSpan.FromSeconds(0.3f));

        playerInput.CannotMove = false;
    }
}
