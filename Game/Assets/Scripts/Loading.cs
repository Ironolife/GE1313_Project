using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    public float rotate_speed = 300.0f;

    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * rotate_speed);
    }
}
