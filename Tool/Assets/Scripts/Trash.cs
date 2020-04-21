using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trash : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Epicenter" || col.tag == "Sensor" || col.tag == "Network" || col.tag == "City")
        {
            Destroy(col.gameObject);

            if(col.tag == "Epicenter")
            {
                GameObject.Find("Panels").transform.Find("ObjectPanel").Find("Grouping").Find("Epicenter").Find("AddButton").GetComponent<Button>().interactable = true;
            }
        }
    }
}
