﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventControl : MonoBehaviour
{
    public float sensor_delay = 0.0f;
    public float network_delay = 1.0f;

    int step = 0;
    string p2_choice_response = "";
    bool p2_choice_validity = false;

    void Start() {

        switch(SceneManager.GetActiveScene().name)
        {
            case "T_1":
            {
                T1();
                break;
            }
            case "P2_1":
            {
                P2_1();
                break;
            }
            case "P2_2":
            {
                P2_2();
                break;
            }
            case "P2_3":
            {
                P2_3();
                break;
            }
        }

    }

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
            case "P1_6":
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

                        if(step == 1)
                        {
                            StartCoroutine(P1_6_2(receiver));
                        }
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
            case "P1_4":
            {
                switch(receiver.name)
                {
                    case "Network":
                    {
                        if(receiver.GetComponent<NetworkStatus>().AddDataPoint() == 3)
                        {
                            receiver.GetComponent<BounceAnimation>().StartBounce();
                            GameObject loading = receiver.transform.Find("Loading").gameObject;
                            loading.SetActive(true);
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
            case "P1_6":
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
                        if(step < 3)
                        {
                            StartCoroutine(P1_6_1(receiver));
                        }
                        else
                        {
                            P1_6_3();
                        }
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

        GameObject[] networks = GameObject.FindGameObjectsWithTag("Network");
        float min_distance = float.MaxValue;
        GameObject target = networks[0];

        for(int i = 0; i < networks.Length; i++)
        {
            float distance = Vector3.Distance(networks[i].transform.position, sensor.transform.position);
            if(distance < min_distance)
            {
                target = networks[i];
                min_distance = distance;
            }
        }

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
            case "Menu":
            {
                SceneManager.LoadScene("P1_1");
                break;
            }
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
            case "P1_3":
            {
                SceneManager.LoadScene("P1_4");
                break;
            }
            case "P1_4":
            {
                SceneManager.LoadScene("P1_5");
                break;
            }
            case "P1_5":
            {
                SceneManager.LoadScene("P1_6");
                break;
            }
            case "P1_6":
            {
                SceneManager.LoadScene("T_1");
                break;
            }
            case "T_1":
            {
                SceneManager.LoadScene("P2_1");
                break;
            }
            case "P2_1":
            {
                SceneManager.LoadScene("P2_2");
                break;
            }
            case "P2_2":
            {
                SceneManager.LoadScene("P2_3");
                break;
            }
            case "P2_3":
            {
                SceneManager.LoadScene("End");
                break;
            }
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
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
        receiver.GetComponent<BounceAnimation>().StartBounce();
        receiver.GetComponent<CityStatus>().SetStatus(true);

        yield return new WaitForSeconds(3f);

        GameObject.Find("Epicenter").transform.Find("Waves").GetComponent<WavesSpread>().Toggle();
        step++;
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
            response.GetComponent<Text>().text = "Incorrect. The seismic waves arrives before the alert.";
        }
        else
        {
            response.GetComponent<Text>().text = "Correct! The seismic waves arrives before the alert.";
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

    IEnumerator P1_6_1(GameObject city)
    {
        GameObject.Find("Epicenter").transform.Find("Waves").GetComponent<WavesSpread>().Toggle();
        GameObject description1 = GameObject.Find("Canvas").transform.Find("Description1").gameObject;
        description1.SetActive(true);

        yield return new WaitForSeconds(2f);

        city.GetComponent<CityStatus>().Reset();
        GameObject.Find("Epicenter").transform.Find("Waves").GetComponent<WavesSpread>().StartShrink();

        GameObject[] sensors = GameObject.FindGameObjectsWithTag("Sensor");
        GameObject[] networks = GameObject.FindGameObjectsWithTag("Network");
        foreach(GameObject sensor in sensors)
        {
            sensor.transform.Find("Data").GetComponent<SendData>().StartReturn();
        }
        foreach(GameObject network in networks)
        {
            network.GetComponent<NetworkStatus>().Reset();
            network.transform.Find("Data").GetComponent<SendData>().StartReturn();
        }

        step++;

        yield return new WaitForSeconds(2f);

        if(step == 1)
        {
            GameObject.Find("Epicenter").transform.position = new Vector3(-7.95f, -1.57f, -5);
            GameObject.Find("Epicenter").transform.Find("Waves").GetComponent<WavesSpread>().Toggle();
        }
        else if(step == 3)
        {
            GameObject.Find("Epicenter").transform.position = new Vector3(-7f, -2.79f, -5);
            GameObject.Find("Epicenter").transform.Find("Waves").GetComponent<WavesSpread>().Toggle();
        }
    }

    IEnumerator P1_6_2(GameObject city)
    {
        GameObject.Find("Epicenter").transform.Find("Waves").GetComponent<WavesSpread>().Toggle();
        GameObject description2 = GameObject.Find("Canvas").transform.Find("Description2").gameObject;
        description2.SetActive(true);

        yield return new WaitForSeconds(2f);

        city.GetComponent<CityStatus>().Reset();
        GameObject.Find("Epicenter").transform.Find("Waves").GetComponent<WavesSpread>().StartShrink();

        GameObject[] sensors = GameObject.FindGameObjectsWithTag("Sensor");
        GameObject[] networks = GameObject.FindGameObjectsWithTag("Network");
        foreach(GameObject sensor in sensors)
        {
            sensor.transform.Find("Data").GetComponent<SendData>().StartReturn();
        }
        foreach(GameObject network in networks)
        {
            network.GetComponent<NetworkStatus>().Reset();
            network.transform.Find("Data").GetComponent<SendData>().StartReturn();
        }

        step++;

        yield return new WaitForSeconds(2f);

        GameObject description3 = GameObject.Find("Canvas").transform.Find("Description3").gameObject;
        description3.SetActive(true);

        for(int i = 0; i < GameObject.Find("Inactive").transform.childCount; i++)
        {
            GameObject.Find("Inactive").transform.GetChild(i).gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(2f);

        GameObject.Find("Epicenter").transform.position = new Vector3(-3f, 3.82f, -5);
        GameObject.Find("Epicenter").transform.Find("Waves").GetComponent<WavesSpread>().Toggle();
    }

    void P1_6_3()
    {
        GameObject.Find("Epicenter").transform.Find("Waves").GetComponent<WavesSpread>().Toggle();
        GameObject.Find("Canvas").transform.Find("RetryButton").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("NextButton").gameObject.SetActive(true);
    }

    void T1()
    {
        StartCoroutine(T1_Delay());
    }

    IEnumerator T1_Delay()
    {
        yield return new WaitForSeconds(5.0f);

        Next();
    }

    void P2_1()
    {
        StartCoroutine(P2_1_Delay());
    }

    IEnumerator P2_1_Delay()
    {
        yield return new WaitForSeconds(1.8f);

        GameObject.Find("Phone").transform.Find("Signal").gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        GameObject.Find("Canvas").transform.Find("PhoneText").gameObject.SetActive(true);
        GameObject.Find("Network").SetActive(false);

        GameObject.Find("Background").transform.Find("Base").gameObject.SetActive(true);
        GameObject.Find("Background").transform.Find("Desks").gameObject.SetActive(true);
        GameObject.Find("Background").transform.Find("Clock").gameObject.SetActive(true);
        GameObject.Find("Background").transform.Find("Shelf").gameObject.SetActive(true);
        GameObject.Find("Background").transform.Find("TeacherDesk").gameObject.SetActive(true);
        GameObject.Find("Background").transform.Find("Door").gameObject.SetActive(true);
        
        yield return new WaitForSeconds(3f);

        GameObject.Find("Background").transform.Find("Shader").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("Title").gameObject.SetActive(true);
        GameObject.Find("Background").transform.Find("Desks").GetComponent<MouseoverGlow>().enabled = true;
        GameObject.Find("Background").transform.Find("Door").GetComponent<MouseoverGlow>().enabled = true;
    }

    void P2_2()
    {
        StartCoroutine(P2_2_Delay());
    }

    IEnumerator P2_2_Delay()
    {
        yield return new WaitForSeconds(1.7f);

        GameObject.Find("Phone").transform.Find("Signal").gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        GameObject.Find("Canvas").transform.Find("PhoneText").gameObject.SetActive(true);
        GameObject.Find("Network").SetActive(false);

        GameObject.Find("Background").transform.Find("Base").gameObject.SetActive(true);
        GameObject.Find("Background").transform.Find("Cart").gameObject.SetActive(true);
        GameObject.Find("Background").transform.Find("Shelves").gameObject.SetActive(true);
        GameObject.Find("Background").transform.Find("Cashier").gameObject.SetActive(true);
        GameObject.Find("Background").transform.Find("Counters").gameObject.SetActive(true);
        GameObject.Find("Background").transform.Find("Door").gameObject.SetActive(true);
        
        yield return new WaitForSeconds(3f);

        GameObject.Find("Background").transform.Find("Shader").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("Title").gameObject.SetActive(true);
        GameObject.Find("Background").transform.Find("Cart").GetComponent<MouseoverGlow>().enabled = true;
        GameObject.Find("Background").transform.Find("Door").GetComponent<MouseoverGlow>().enabled = true;
        GameObject.Find("Background").transform.Find("Shelves").GetComponent<MouseoverGlow>().enabled = true;
    }

    void P2_3()
    {
        StartCoroutine(P2_3_Delay());
    }

    IEnumerator P2_3_Delay()
    {
        yield return new WaitForSeconds(1.7f);

        GameObject.Find("Phone").transform.Find("Signal").gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        GameObject.Find("Canvas").transform.Find("PhoneText").gameObject.SetActive(true);
        GameObject.Find("Network").SetActive(false);

        GameObject.Find("Background").transform.Find("Base").gameObject.SetActive(true);
        GameObject.Find("Background").transform.Find("Door").gameObject.SetActive(true);
        GameObject.Find("Background").transform.Find("Window").gameObject.SetActive(true);
        GameObject.Find("Background").transform.Find("Table").gameObject.SetActive(true);
        GameObject.Find("Background").transform.Find("Sofa").gameObject.SetActive(true);
        GameObject.Find("Background").transform.Find("CoffeeTable").gameObject.SetActive(true);
        GameObject.Find("Background").transform.Find("Plant").gameObject.SetActive(true);
        
        yield return new WaitForSeconds(3f);

        GameObject.Find("Background").transform.Find("Shader").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("Title").gameObject.SetActive(true);
        GameObject.Find("Background").transform.Find("Door").GetComponent<MouseoverGlow>().enabled = true;
        GameObject.Find("Background").transform.Find("Window").GetComponent<MouseoverGlow>().enabled = true;
        GameObject.Find("Background").transform.Find("Table").GetComponent<MouseoverGlow>().enabled = true;
        GameObject.Find("Background").transform.Find("Sofa").GetComponent<MouseoverGlow>().enabled = true;
    }

    public void P2_Choice(string response_text, bool validity)
    {
        GameObject.Find("Background").transform.Find("Shader").GetComponent<FadeAnimation>().FadeOut();
        if(p2_choice_response == "")
        {
            GameObject.Find("Canvas").transform.Find("PhoneText").Find("TimeValue").GetComponent<Countdown>().StartTickToZero();
        }
        GameObject.Find("Canvas").transform.Find("Title").GetComponent<FadeAnimation>().FadeOut();
        p2_choice_response = response_text;
        p2_choice_validity = validity;
    }

    public void P2_Shake()
    {
        GameObject.Find("Background").GetComponent<QuakeAnimation>().StartQuake();
    }

    public void P2_EndShake()
    {
        GameObject.Find("Canvas").transform.Find("Response").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("Response").GetComponent<Text>().text = p2_choice_response;

        GameObject phone_text = GameObject.Find("Canvas").transform.Find("PhoneText").gameObject;
        for(int i = 0; i < phone_text.transform.childCount; i++)
        {
            phone_text.transform.GetChild(i).GetComponent<FadeAnimation>().FadeOut();
        }

        GameObject.Find("Canvas").transform.Find("RetryButton").gameObject.SetActive(true);

        if(p2_choice_validity == true)
        {
            GameObject.Find("Canvas").transform.Find("NextButton").gameObject.SetActive(true);
        }
        else
        {
            GameObject.Find("Phone").transform.Find("Crack").gameObject.SetActive(true);
        }
    }
}
