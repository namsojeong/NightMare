using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    float rotateSpeedX = 3;
    float rotateSpeedY = 5;
    float minX = -80;
    float maxX = 80;
    float eulerAngleX;
    float eulerAngleY;

    private void Update()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        RotateCamera(x, y);
    }
    void RotateCamera(float mouseX, float mouseY)
    {
        eulerAngleY += mouseX * rotateSpeedX;
        eulerAngleX += mouseY * rotateSpeedY;

       // float angle = eulerAngleX;

       // if (angle < -360) angle += 360;
      //  if (angle > -360) angle -= 360;

        eulerAngleX = Mathf.Clamp(eulerAngleX, minX, maxX);

        transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY , 0);
    }

}
