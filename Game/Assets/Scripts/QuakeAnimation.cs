using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuakeAnimation : MonoBehaviour
{
    public int intensity = 1;

    public void StartQuake()
    {
        StartCoroutine(Quake());
    }

    IEnumerator Quake()
    {
        float accumated_time = 0.0f;
        float time = 0.0f;
        Vector2 random_direction = Random.insideUnitCircle.normalized;
        int step = 0;
        while(accumated_time < 3.0f)
        {
            accumated_time += Time.deltaTime;
            time += Time.deltaTime;

            Vector3 old_position = gameObject.transform.position;
            old_position.x += random_direction.x * Time.deltaTime * (0.5f + intensity * 0.2f);
            old_position.y += random_direction.y * Time.deltaTime * (0.5f + intensity * 0.2f);
            gameObject.transform.position = old_position;

            if(time >= 0.1f)
            {
                if(step == 0)
                {
                    random_direction = -random_direction;
                    step = 1;
                }
                else if(step == 1)
                {
                    random_direction = Random.insideUnitCircle.normalized;
                    step = 0;
                }
                time = 0.0f;
            }

            yield return null;
        }
        
        GameObject.Find("EventSystem").GetComponent<EventControl>().P2_EndShake();
    }
}
