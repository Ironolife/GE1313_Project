using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Spawner : MonoBehaviour
{
    public GameObject epicenter;
    public GameObject sensor;
    public GameObject network;
    public GameObject city;

    public void Spawn(int type)
    {
        switch(type)
        {
            case 0:
            {
                Vector3 position = transform.position;
                position.z = -10.0f;
                Instantiate(epicenter, position, Quaternion.identity);

                GameObject.Find("Panels").transform.Find("ObjectPanel").Find("Grouping").Find("Epicenter").Find("AddButton").GetComponent<Button>().interactable = false;
                
                break;
            }
            case 1:
            {
                Vector3 position = transform.position;
                position.z = -15.0f;
                Instantiate(sensor, position, Quaternion.identity);
                
                break;
            }
            case 2:
            {
                Vector3 position = transform.position;
                position.z = -15.0f;
                Instantiate(network, position, Quaternion.identity);
                
                break;
            }
            case 3:
            {
                Vector3 position = transform.position;
                position.z = -20.0f;
                Instantiate(city, position, Quaternion.identity);
                
                break;
            }
        }
    }

    public void Reset()
    {
        GameObject[] epicenters = GameObject.FindGameObjectsWithTag("Epicenter");
        GameObject[] sensors = GameObject.FindGameObjectsWithTag("Sensor");
        GameObject[] networks = GameObject.FindGameObjectsWithTag("Network");
        GameObject[] cities = GameObject.FindGameObjectsWithTag("City");

        GameObject[] all = epicenters.Concat(sensors).Concat(networks).Concat(cities).ToArray();

        foreach (GameObject toBeDestroy in all)
        {
            string tag = toBeDestroy.tag;
            Vector3 position = toBeDestroy.transform.position;
            Destroy(toBeDestroy);

            switch(tag)
            {
                case "Epicenter":
                {
                    Instantiate(epicenter, position, Quaternion.identity);
                    break;
                }
                case "Sensor":
                {
                    Instantiate(sensor, position, Quaternion.identity);
                    break;
                }
                case "Network":
                {
                    Instantiate(network, position, Quaternion.identity);
                    break;
                }
                case "City":
                {
                    Instantiate(city, position, Quaternion.identity);
                    break;
                }
            }
        }
    }
}
