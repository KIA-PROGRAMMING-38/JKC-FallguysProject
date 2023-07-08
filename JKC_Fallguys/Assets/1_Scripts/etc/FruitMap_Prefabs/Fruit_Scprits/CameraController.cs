using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _rotateSpeedX;
    [SerializeField] private float _rotateSpeedY;
    private float _limitMinX = -80f;
    private float _limitMaxX = 50f;
    private float eulerAngleX;
    private float eulerAngleY;

    public void RotateTo(float mouseX, float mouseY)
    {
        eulerAngleY += mouseX * _rotateSpeedX;
        eulerAngleX -= mouseY * _rotateSpeedY;
        eulerAngleX = ClampAngle(eulerAngleX, _limitMinX, _limitMaxX);

        transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
   
}
