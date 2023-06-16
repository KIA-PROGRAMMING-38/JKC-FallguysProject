using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Axe : MonoBehaviour
{
    [SerializeField] private float _axeRotationSpeed = 100f;
    private void Update()
    {
        transform.Rotate(Vector3.forward * _axeRotationSpeed * Time.deltaTime);
    }
}
