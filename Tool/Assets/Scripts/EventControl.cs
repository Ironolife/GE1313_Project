using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventControl : MonoBehaviour
{
    public bool can_drag = true;
    public bool can_quake = false;

    public GameObject wave_speed_input;
    public GameObject data_speed_input;
    public GameObject network_delay_input;
    public float wave_speed = 3.0f;
    public float data_speed = 5.0f;
    public float network_delay = 1.0f;

    public GameObject report;
    int total_city_count;
    float total_alert_time = 0.0f;
    int city_report_count = 0;
    int failed_report_count = 0;
    int success_report_count = 0;

    public void ChangeWaveSpeed()
    {
        wave_speed = float.Parse(wave_speed_input.GetComponent<InputField>().text);
    }

    public void ChangeDataSpeed()
    {
        data_speed = float.Parse(data_speed_input.GetComponent<InputField>().text);
    }
    
    public void ChangeNetworkDelay()
    {
        network_delay = float.Parse(network_delay_input.GetComponent<InputField>().text);
    }

    public void ToggleDrag()
    {
        can_drag = !can_drag;
    }

    public void StartSimulation()
    {
        GameObject[] epicenters = GameObject.FindGameObjectsWithTag("Epicenter");

        foreach (GameObject epicenter in epicenters)
        {
            epicenter.transform.Find("Quake").gameObject.SetActive(true);
        }

        can_quake = true;

        total_city_count = GameObject.FindGameObjectsWithTag("City").Length;
    }

    public void ReportTime(float positive_time, float negative_time)
    {
        string message;

        if(positive_time > 0)
        {
            message = "City " + (city_report_count + 1) + ": " + (negative_time - positive_time).ToString("F2") + " s";
            success_report_count++;
            total_alert_time += (negative_time - positive_time);
        }
        else
        {
            message = "City " + (city_report_count + 1) + ": " + "Failed";
            failed_report_count++;
        }

        AddReportText(message);

        city_report_count++;

        if(city_report_count == total_city_count)
        {
            AddReportText("Success: " + success_report_count + ", Failed: " + failed_report_count);
            AddReportText("Average: " + (total_alert_time / total_city_count).ToString("F2") + " s");
        }
    }

    void AddReportText(string message)
    {
        string old = report.GetComponent<Text>().text;
        old += message + "\n\n";
        report.GetComponent<Text>().text = old;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Reset()
    {
        can_drag = true;
        can_quake = false;
        total_alert_time = 0.0f;
        city_report_count = 0;
        failed_report_count = 0;
        success_report_count = 0;
        report.GetComponent<Text>().text = "";

        GameObject.Find("Spawner").GetComponent<Spawner>().Reset();
    }
}
