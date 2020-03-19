using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectWaves : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name == "Waves")
        {
            GameObject.Find("EventSystem").GetComponent<EventControl>().WavesDetected(gameObject);
        }
    }
}
