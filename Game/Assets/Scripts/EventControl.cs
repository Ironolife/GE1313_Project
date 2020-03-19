using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventControl : MonoBehaviour
{
    public float sensor_delay = 0.0f;
    public float network_delay = 1.0f;

    int step = 0;

    public void WavesDetected(GameObject receiver)
    {
        Debug.Log(receiver.name + " detected waves.");

        switch(SceneManager.GetActiveScene().name)
        {
            case "P1_1":
            {
                switch(receiver.name)
                {
                    case "Sensor":
                    {
                        if(step == 0)
                        {
                            StartCoroutine(P1_1_1(receiver));
                        }
                        else
                        {
                            receiver.GetComponent<BounceAnimation>().StartBounce();
                            StartCoroutine(SensorDelay(receiver));
                        }
                        break;
                    }
                    case "City":
                    {
                        receiver.GetComponent<BounceAnimation>().StartBounce();
                        receiver.GetComponent<CityStatus>().SetStatus(false);
                        break;
                    }
                }
                break;
            }
            case "P1_2":
            {
                switch(receiver.name)
                {
                    case "Sensor":
                    {
                        if(step == 0)
                        {
                            StartCoroutine(P1_1_1(receiver));
                        }
                        else
                        {
                            receiver.GetComponent<BounceAnimation>().StartBounce();
                            StartCoroutine(SensorDelay(receiver));
                        }
                        break;
                    }
                    case "City":
                    {
                        receiver.GetComponent<BounceAnimation>().StartBounce();
                        receiver.GetComponent<CityStatus>().SetStatus(false);
                        P1_2_2();
                        break;
                    }
                }
                break;
            }
            case "P1_3":
            {
                switch(receiver.name)
                {
                    case "Sensor":
                    {
                        receiver.GetComponent<BounceAnimation>().StartBounce();
                        StartCoroutine(SensorDelay(receiver));
                        break;
                    }
                    case "City":
                    {
                        receiver.GetComponent<BounceAnimation>().StartBounce();
                        receiver.GetComponent<CityStatus>().SetStatus(false);
                        step--;
                        P1_3_2();
                        break;
                    }
                }
                break;
            }
            default:
            {
                switch(receiver.name)
                {
                    case "Sensor":
                    {
                        receiver.GetComponent<BounceAnimation>().StartBounce();
                        StartCoroutine(SensorDelay(receiver));
                        break;
                    }
                    case "City":
                    {
                        receiver.GetComponent<BounceAnimation>().StartBounce();
                        receiver.GetComponent<CityStatus>().SetStatus(false);
                        break;
                    }
                }
                break;
            }
        }
    }

    public void DataReceived(GameObject receiver)
    {
        Debug.Log(receiver.name + " received data.");

        switch(SceneManager.GetActiveScene().name)
        {
            case "P1_1":
            {
                switch(receiver.name)
                {
                    case "Network":
                    {
                        if(receiver.GetComponent<NetworkStatus>().AddDataPoint() == 3)
                        {
                            StartCoroutine(P1_1_2(receiver));
                        }
                        break;
                    }
                    case "City":
                    {
                        StartCoroutine(P1_1_3(receiver));
                        break;
                    }
                }
                break;
            }
            case "P1_3":
            {
                switch(receiver.name)
                {
                    case "Network":
                    {
                        if(receiver.GetComponent<NetworkStatus>().AddDataPoint() == 3)
                        {
                            receiver.GetComponent<BounceAnimation>().StartBounce();
                            StartCoroutine(NetworkDelay(receiver));
                        }
                        break;
                    }
                    case "City":
                    {
                        receiver.GetComponent<CircleCollider2D>().enabled = false;
                        receiver.GetComponent<BounceAnimation>().StartBounce();
                        receiver.GetComponent<CityStatus>().SetStatus(true);
                        step++;
                        P1_3_2();
                        break;
                    }
                }
                break;
            }
            default:
            {
                switch(receiver.name)
                {
                    case "Network":
                    {
                        if(receiver.GetComponent<NetworkStatus>().AddDataPoint() == 3)
                        {
                            receiver.GetComponent<BounceAnimation>().StartBounce();
                            StartCoroutine(NetworkDelay(receiver));
                        }
                        break;
                    }
                    case "City":
                    {
                        receiver.GetComponent<BounceAnimation>().StartBounce();
                        receiver.GetComponent<CityStatus>().SetStatus(true);
                        break;
                    }
                }
                break;
            }
        }
    }

    IEnumerator SensorDelay(GameObject sensor)
    {
        GameObject loading = sensor.transform.Find("Loading").gameObject;
        loading.SetActive(true);
        yield return new WaitForSeconds(sensor_delay);
        loading.SetActive(false);

        GameObject target = GameObject.Find("Network");
        sensor.transform.Find("Data").GetComponent<SendData>().StartSend(target);
    }

    IEnumerator NetworkDelay(GameObject network)
    {
        GameObject loading = network.transform.Find("Loading").gameObject;
        loading.SetActive(true);
        yield return new WaitForSeconds(network_delay);
        loading.SetActive(false);

        GameObject target = GameObject.Find("City");
        network.transform.Find("Data").GetComponent<SendData>().StartSend(target);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Next()
    {
        switch(SceneManager.GetActiveScene().name)
        {
            case "P1_1":
            {
                SceneManager.LoadScene("P1_2");
                break;
            }
            case "P1_2":
            {
                SceneManager.LoadScene("P1_3");
                break;
            }
        }
    }

    IEnumerator P1_1_1(GameObject receiver)
    {
        GameObject.Find("Epicenter").transform.Find("Waves").GetComponent<WavesSpread>().Toggle();
        GameObject.Find("Canvas").transform.Find("Description2").gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        GameObject.Find("Epicenter").transform.Find("Waves").GetComponent<WavesSpread>().Toggle();
        step++;
        
        receiver.GetComponent<BounceAnimation>().StartBounce();
        StartCoroutine(SensorDelay(receiver));
    }

    IEnumerator P1_1_2(GameObject receiver)
    {
        GameObject.Find("Epicenter").transform.Find("Waves").GetComponent<WavesSpread>().Toggle();
        GameObject.Find("Canvas").transform.Find("Description3").gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        GameObject.Find("Epicenter").transform.Find("Waves").GetComponent<WavesSpread>().Toggle();
        step++;

        receiver.GetComponent<BounceAnimation>().StartBounce();
        StartCoroutine(NetworkDelay(receiver));
    }

    IEnumerator P1_1_3(GameObject receiver)
    {
        GameObject.Find("Epicenter").transform.Find("Waves").GetComponent<WavesSpread>().Toggle();
        GameObject.Find("Canvas").transform.Find("Description4").gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        GameObject.Find("Epicenter").transform.Find("Waves").GetComponent<WavesSpread>().Toggle();
        step++;

        receiver.GetComponent<BounceAnimation>().StartBounce();
        receiver.GetComponent<CityStatus>().SetStatus(true);
        GameObject.Find("Canvas").transform.Find("RetryButton").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("NextButton").gameObject.SetActive(true);
    }

    public void P1_2_1(GameObject buttonClicked)
    {
        if(buttonClicked.name == "YesButton")
        {
            step++;
        }
        else
        {
            step--;
        }

        GameObject.Find("Epicenter").transform.Find("Waves").GetComponent<WavesSpread>().Toggle();
    }

    public void P1_2_2()
    {
        GameObject.Find("Epicenter").transform.Find("Waves").GetComponent<WavesSpread>().Toggle();

        GameObject response = GameObject.Find("Canvas").transform.Find("Response").gameObject;

        if(step == 1)
        {
            response.GetComponent<Text>().text = "Incorrect. The waves arrives before the alert.";
        }
        else
        {
            response.GetComponent<Text>().text = "Correct! The waves arrives before the alert.";
        }

        response.SetActive(true);
        GameObject.Find("Canvas").transform.Find("NextButton").gameObject.SetActive(true);
    }

    public void P1_3_1()
    {
        GameObject.Find("Epicenter").transform.Find("Waves").GetComponent<WavesSpread>().Toggle();
    }

    public void P1_3_2()
    {
        GameObject.Find("Epicenter").transform.Find("Waves").GetComponent<WavesSpread>().Toggle();

        GameObject response1 = GameObject.Find("Canvas").transform.Find("Response1").gameObject;

        if(step == 1)
        {
            response1.GetComponent<Text>().text = "Well Done!";
            response1.SetActive(true);
            GameObject response2 = GameObject.Find("Canvas").transform.Find("Response2").gameObject;
            response2.SetActive(true);
            
            GameObject.Find("Canvas").transform.Find("RetryButton").gameObject.GetComponent<Button>().interactable = true;
            GameObject.Find("Canvas").transform.Find("NextButton").gameObject.SetActive(true);
        }
        else
        {
            response1.GetComponent<Text>().text = "Failed! Try Again.";
            response1.SetActive(true);
            
            GameObject.Find("Canvas").transform.Find("RetryButton").gameObject.GetComponent<Button>().interactable = true;
        }
    }
}
