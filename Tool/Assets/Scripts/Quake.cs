using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quake : MonoBehaviour
{
    EventControl event_control;

    void Start()
    {
        event_control = GameObject.Find("EventSystem").GetComponent<EventControl>();

        StartCoroutine(Spread());
    }

    IEnumerator Spread()
    {
        while(true)
        {
            if(event_control.can_quake)
            {
                transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * event_control.wave_speed;
            }

            yield return null;
        }
    }
}
