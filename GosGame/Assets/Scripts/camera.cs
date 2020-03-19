using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public float speedH = 10.0f;
    public float speedV = 10.0f;

    private float rotX = 0.0f;
    private float rotY = 0.0f;

    private float minVert = 45.0f;
    private float maxVert = 135.0f;


    public enum RotationAxis
    {
        MouseX = 1,
        MouseY = 2,
        MouseZ = 3
    }

    public RotationAxis axes = RotationAxis.MouseX;


    void Update()
    {
        if(axes == RotationAxis.MouseX)
        {
            transform.Rotate(0, Input.GetAxisRaw("Mouse X") * speedH, 0);
        }
        else if (axes == RotationAxis.MouseZ)
        {
            transform.Rotate(0, 0, Input.GetAxisRaw("Mouse X") * speedH);
        }
        else if(axes == RotationAxis.MouseY)
        {
            rotX -= Input.GetAxisRaw("Mouse Y") * speedV;
            rotX = Mathf.Clamp(rotX, minVert, maxVert);
            //rotY = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(rotX, 0, 0);
        }
    }
}
