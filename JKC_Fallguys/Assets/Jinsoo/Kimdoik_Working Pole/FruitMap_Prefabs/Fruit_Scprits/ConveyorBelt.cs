using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float speed;
    private Rigidbody _conveyorBeltRigidbody;
    void Awake()
    {
        _conveyorBeltRigidbody= GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 position = _conveyorBeltRigidbody.position;
        _conveyorBeltRigidbody.position += Vector3.forward * speed * Time.deltaTime;
        _conveyorBeltRigidbody.MovePosition(position);
    }
}
