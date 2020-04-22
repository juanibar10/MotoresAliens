using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    public Vector3 localRotation;


    private void FixedUpdate()
    {
        transform.Rotate(localRotation * Time.fixedDeltaTime);
    }
}
