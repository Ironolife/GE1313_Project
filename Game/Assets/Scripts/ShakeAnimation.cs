using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeAnimation : MonoBehaviour
{
    public bool start_direction = true;

    void Start()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float time = 0.0f;

        if(start_direction == false)
        {
            time = 2.25f;
        }

        while(true)
        {
            if(time < 1.5f)
            {
                transform.position += new Vector3(0, 1, 0) * Time.deltaTime * 0.075f;
            }
            else
            {
                transform.position -= new Vector3(0, 1, 0) * Time.deltaTime * 0.075f;
            }

            time += Time.deltaTime;

            if(time >= 3.0f)
            {
                time = 0.0f;
            }

            yield return null;
        }
    }
}
