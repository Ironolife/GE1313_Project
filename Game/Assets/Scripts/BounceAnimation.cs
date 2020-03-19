using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceAnimation : MonoBehaviour
{
    public void StartBounce()
    {
        StartCoroutine(Bounce());
    }

    IEnumerator Bounce()
    {
        float time = 0.0f;

        while(time < 0.6f)
        {
            if(time < 0.3f)
            {
                transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * 0.1f;
            }
            else
            {
                transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * 0.1f;
            }

            time += Time.deltaTime;

            yield return null;
        }
    }
}
