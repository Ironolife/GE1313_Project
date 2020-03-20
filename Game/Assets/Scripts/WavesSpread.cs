using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesSpread : MonoBehaviour
{
    public float spread_speed = 3.0f;
    public float max_scale = 40.0f;
    public bool is_paused = true;

    void Start()
    {
        StartCoroutine(Quake());
    }

    IEnumerator Quake()
    {
        while(transform.localScale.x <= max_scale)
        {
            if(!is_paused)
            {
                transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * spread_speed;
            }

            yield return null;
        }
    }

    public void Toggle()
    {
        is_paused = !is_paused;
    }

    public void StartShrink()
    {
        StartCoroutine(Shrink());
    }

    IEnumerator Shrink()
    {
        while(transform.localScale.x > 0)
        {
            transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * spread_speed * 3;

            if(transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(0, 0, 1);
            }

            yield return null;
        }
    }
}
