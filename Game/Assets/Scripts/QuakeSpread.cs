using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesSpread : MonoBehaviour
{
    public float spread_speed = 3.0f;
    public float max_scale = 100.0f;
    private bool is_paused = true;

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
}
