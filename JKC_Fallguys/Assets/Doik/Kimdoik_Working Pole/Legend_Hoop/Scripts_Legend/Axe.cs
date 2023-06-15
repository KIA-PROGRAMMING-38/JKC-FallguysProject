using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Axe : MonoBehaviour
{
    [SerializeField] private float _axeRotationSpeed = 100f;
    public float MinimuCollisionValue;
    public float reflectForce;

    private Rigidbody _axeRigidbody;
    private void Awake()
    {
        _axeRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward * _axeRotationSpeed * Time.deltaTime);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Immobilized = false;
            Rigidbody collisionObjectRigid = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 collisionVector = collisionObjectRigid.velocity.normalized;
            Vector3 normalVector = collision.contacts[0].normal.normalized;
            Vector3 reflectionDirection =
                Vector3.Reflect(collisionVector, normalVector);

            float collisionImpulseForce = collision.impulse.magnitude;
            collisionImpulseForce = Mathf.Max(MinimuCollisionValue, collisionImpulseForce);

            Debug.Log($"impulseMagnitude : {collision.impulse.magnitude}");
            Debug.Log($"collisionImpulseForce : {collisionImpulseForce}");


            Debug.Log($"입사각 : {collisionVector}");
            Debug.Log($"충돌 법선 벡터 : {collision.contacts[0].normal}");
            Debug.Log($"반사각 : {reflectionDirection}");

            collisionObjectRigid.AddForce(reflectionDirection * reflectForce * collisionImpulseForce, ForceMode.Impulse);
        }
    }
}