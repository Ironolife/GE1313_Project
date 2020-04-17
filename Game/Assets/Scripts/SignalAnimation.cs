using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalAnimation : MonoBehaviour
{
    public Sprite signal_1;
    public Sprite signal_2;
    int current = 0;
    public float duration = 2.0f;

    void Start()
    {
        StartCoroutine(Signal());
    }

    IEnumerator Signal()
    {
        float time = 0;
        float accumulated_time = 0;

        SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();

        while(accumulated_time < duration) {

            time += Time.deltaTime;
            accumulated_time += Time.deltaTime;

            if(time > 0.2f)
            {
                switch(current)
                {
                    case 0:
                    {
                        sprite_renderer.flipX = true;
                        current = 1;
                        gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 1);
                        break;
                    }
                    case 1:
                    {
                        sprite_renderer.flipX = false;
                        sprite_renderer.sprite = signal_2;
                        current = 2;
                        gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1);
                        break;
                    }
                    case 2:
                    {
                        sprite_renderer.flipX = true;
                        current = 3;
                        gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 1);
                        break;
                    }
                    case 3:
                    {
                        sprite_renderer.sprite = signal_1;
                        sprite_renderer.flipX = false;
                        current = 0;
                        gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1);
                        break;
                    }
                }

                time = 0;

            }
                
            yield return null;

        }

        gameObject.SetActive(false);
    }
}
