using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitySignal : MonoBehaviour
{
    int status = 0;
    float positive_time = -1.0f;
    float negative_time = -1.0f;

    EventControl event_control;

    void Start()
    {
        event_control = GameObject.Find("EventSystem").GetComponent<EventControl>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Quake")
        {
            negative_time = Time.time;

            if(status == 0)
            {
                GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);

                status = -1;

            }

            event_control.ReportTime(positive_time, negative_time);
        }
    }

    public void ReceiveSignal()
    {
        positive_time = Time.time;
        if(status == 0)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.0f, 1.0f, 0.0f, 1.0f);

            status = 1;
        }
    }
}
