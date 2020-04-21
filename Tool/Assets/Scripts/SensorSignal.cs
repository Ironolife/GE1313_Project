using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorSignal : MonoBehaviour
{
    EventControl event_control;

    void Start()
    {
        event_control = GameObject.Find("EventSystem").GetComponent<EventControl>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Quake")
        {
            GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
            
            GameObject[] networks = GameObject.FindGameObjectsWithTag("Network");

            foreach (GameObject network in networks)
            {
                GameObject data = new GameObject("Data");

                data.transform.parent = gameObject.transform;

                Vector3 position = transform.position;
                position.z += 1;
                data.transform.position = position;

                LineRenderer line_renderer = data.AddComponent(typeof(LineRenderer)) as LineRenderer;
                line_renderer.startWidth = line_renderer.endWidth = 0.05f;
                line_renderer.startColor = line_renderer.endColor = new Color(1.0f, 1.0f, 0.0f, 1.0f);
                line_renderer.material = new Material(Shader.Find("Sprites/Default"));
                line_renderer.SetPositions(new Vector3[] {position, position});

                StartCoroutine(Send(network, line_renderer));
            }
        }
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
                target.GetComponent<NetworkSignal>().ReceiveSignal();
            }

            yield return null;
        }
    }
}
