using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour
{
    public float rotationSpeed;
    private Vector3 _PropellerRotation;
    void Start()
    {
        _PropellerRotation = Vector3.up;
    }
    void Update()
    {
        transform.Rotate(_PropellerRotation, rotationSpeed * Time.deltaTime);
    }

}
