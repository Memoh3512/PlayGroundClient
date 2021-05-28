using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCameraControls : ICameraControls
{
    public float sensitivity = 5;
    public void MoveCamera(GameObject head)
    {
        head.transform.Rotate(-Input.GetAxis("Mouse Y")*sensitivity, 0, 0);
        head.transform.Rotate(0, Input.GetAxis("Mouse X")*sensitivity, 0);
        head.transform.eulerAngles = new Vector3(Clampcamera(head.transform.eulerAngles.x), head.transform.eulerAngles.y, 0);

    }

    private float Clampcamera(float x)
    {

        if (x > 90)
        {

            return Mathf.Clamp(x, 271, 360);

        }
        else
        {

            return Mathf.Clamp(x, 0, 89);

        }

    }
}
