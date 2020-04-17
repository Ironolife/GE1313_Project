using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyWave : MonoBehaviour
{
    public float delay = 0.0f;
    public float spread_speed = 2.0f;

    void Start()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);

        StartCoroutine(Spread());
    }

    IEnumerator Spread()
    {
        while(true)
        {
            transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * spread_speed;

            yield return null;
        }
    }
}
