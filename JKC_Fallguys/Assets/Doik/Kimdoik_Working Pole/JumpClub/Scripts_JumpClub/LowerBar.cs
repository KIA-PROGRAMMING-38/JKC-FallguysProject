using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerBar : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float acceleration;
    private Rigidbody _LowerBar;

    private void Awake()
    {
        _LowerBar = GetComponent<Rigidbody>();
    }
    void Start()
    {
        StartCoroutine(IncreaseRotationSpeed());
    }
    void Update()
    {
        _LowerBar.AddTorque(Vector3.up * rotationSpeed);
    }
    IEnumerator IncreaseRotationSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // 1�ʸ��� ȸ�� �ӵ��� ������Ŵ
            rotationSpeed += acceleration;
        }
    }
}
