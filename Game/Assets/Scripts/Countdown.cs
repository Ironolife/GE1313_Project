using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public int timer = 60;
    public int stopTime = 57;
    Text text;
    public bool increase_intensity = false;

    void Start()
    {
        text = GetComponent<Text>();

        StartCoroutine(Tick());
    }

    IEnumerator Tick()
    {
        float time = 0.0f;

        while(timer > stopTime) {
            time += Time.deltaTime;

            if(time >= 1.0f)
            {
                timer -= 1;
                text.text = timer + " s";
                time = 0;
            }
            yield return null;
        }

    }

    public void StartTickToZero()
    {
        StartCoroutine(TickToZero());
    }

    IEnumerator TickToZero()
    {
        float time = 0.0f;
        float accumulated_time = 1.0f;

        Text intensity_text = GameObject.Find("Canvas").transform.Find("PhoneText").transform.Find("IntensityValue").GetComponent<Text>();

        while(timer > 0) {
            accumulated_time += Time.deltaTime * 6.0f;
            time += Time.deltaTime * accumulated_time;

            if(time >= 1.0f)
            {
                timer -= 1;
                text.text = timer + " s";
                time = 0;
            }

            if(increase_intensity)
            {
                switch(timer) {
                    case int n when (n >= 30): {
                        intensity_text.text = "I";
                        break;
                    }
                    case int n when (n < 30 && n >= 26): {
                        intensity_text.text = "II";
                        break;
                    }
                    case int n when (n < 26 && n >= 22): {
                        intensity_text.text = "III";
                        break;
                    }
                    case int n when (n < 22 && n >= 18): {
                        intensity_text.text = "IV";
                        break;
                    }
                    case int n when (n < 18 && n >= 14): {
                        intensity_text.text = "V";
                        break;
                    }
                    case int n when (n < 14 && n >= 10): {
                        intensity_text.text = "VI";
                        break;
                    }
                    case int n when (n < 10 && n >= 6): {
                        intensity_text.text = "VII";
                        break;
                    }
                    case int n when (n < 6): {
                        intensity_text.text = "VIII";
                        break;
                    }
                }
            }

            yield return null;
        }

        GameObject.Find("EventSystem").GetComponent<EventControl>().P2_Shake();

    }

}
