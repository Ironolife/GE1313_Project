using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkSignal : MonoBehaviour
{
    int signal_received = 0;

    EventControl event_control;

    void Start()
    {
        event_control = GameObject.Find("EventSystem").GetComponent<EventControl>();
    }
    
    public void ReceiveSignal()
    {
        signal_received++;

        if(signal_received == 3)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.0f, 1.0f, 1.0f, 1.0f);

            GameObject[] cities = GameObject.FindGameObjectsWithTag("City");

            foreach (GameObject city in cities)
            {
                GameObject data = new GameObject("Data");

                data.transform.parent = gameObject.transform;

                Vector3 position = transform.position;
                position.z += 1;
                data.transform.position = position;

                LineRenderer line_renderer = data.AddComponent(typeof(LineRenderer)) as LineRenderer;
                line_renderer.startWidth = line_renderer.endWidth = 0.05f;
                line_renderer.startColor = line_renderer.endColor = new Color(0.0f, 1.0f, 1.0f, 1.0f);
                line_renderer.material = new Material(Shader.Find("Sprites/Default"));
                line_renderer.SetPositions(new Vector3[] {position, position});

                StartCoroutine(Delay(city, line_renderer));
            }
        }
    }

    IEnumerator Delay(GameObject target, LineRenderer line_renderer)
    {
        yield return new WaitForSeconds(event_control.network_delay);

        StartCoroutine(Send(target, line_renderer));
    }

    IEnumerator Send(GameObject target, LineRenderer line_renderer)
    {
        Vector3 target_position = target.transform.position;
        Vector3 sender_position = line_renderer.GetPosition(0);
        target_position.z = sender_position.z;

        Vector3 difference = target_position - sender_position;
        float distance = difference.magnitude;
        Vector3 direction = difference / distance;

        bool reached = false;
        while(!reached)
        {
            Vector3 end = line_renderer.GetPosition(1);
            end += Time.deltaTime * direction * event_control.data_speed;
            line_renderer.SetPosition(1, end);

            if((end - sender_position).magnitude > distance)
            {
                reached = true;
                target.GetComponent<CitySignal>().ReceiveSignal();
            }

            yield return null;
        }
    }
}
