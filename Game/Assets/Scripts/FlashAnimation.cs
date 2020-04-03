using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashAnimation : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        bool direction = false;

        while(true)
        {
            Color color = gameObject.GetComponent<Image>().color;

            if(direction)
            {
                color.a += Time.deltaTime;
                gameObject.GetComponent<Image>().color = color;
            }
            else
            {
                color.a -= Time.deltaTime;
                gameObject.GetComponent<Image>().color = color;
            }

            if(direction && color.a >= 0.7f)
            {
                direction = !direction;
            }
            else if(!direction && color.a <= 0.0f)
            {
                direction = !direction;
            }

            yield return null;
        }
    }
}
