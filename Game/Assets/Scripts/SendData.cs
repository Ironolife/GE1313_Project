using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendData : MonoBehaviour
{
    LineRenderer line_renderer;
    public float data_speed = 5.0f;

    void Start()
    {
        line_renderer = gameObject.GetComponent<LineRenderer>();

        Color color = gameObject.transform.parent.GetComponent<SpriteRenderer>().color;
        color.a = 1;
        line_renderer.startColor = line_renderer.endColor = color;

        Vector3 original_position = gameObject.transform.parent.transform.position;
        original_position.z += 2f;
        line_renderer.SetPositions(new Vector3[]{original_position, original_position});
    }

    public void StartSend(GameObject target)
    {
        StartCoroutine(Send(target));
    }

    IEnumerator Send(GameObject target)
    {
        Vector3 target_position = target.transform.position;
        target_position.z += 2f;
        Vector3 original_position = line_renderer.GetPosition(0);

        Vector3 difference = target_position - original_position;
        float distance = difference.magnitude;
        Vector3 direction = difference / distance;

        Debug.Log("!");

        bool reached = false;
        while(!reached)
        {
            Vector3 end = line_renderer.GetPosition(1);
            end += Time.deltaTime * direction * data_speed;
            line_renderer.SetPosition(1, end);

            if((end - original_position).magnitude > distance)
            {
                reached = true;

                GameObject.Find("EventSystem").GetComponent<EventControl>().DataReceived(target);
            }

            yield return null;
        }
    }

    public void StartReturn()
    {
        StartCoroutine(Return());
    }

    IEnumerator Return()
    {
        Vector3 target_position = line_renderer.GetPosition(1);
        Vector3 original_position = line_renderer.GetPosition(0);

        Vector3 difference = target_position - original_position;
        float distance = difference.magnitude;
        Vector3 direction = difference / distance;

        bool done = false;
        while(!done && !target_position.Equals(original_position))
        {
            Vector3 end = line_renderer.GetPosition(1);
            end -= Time.deltaTime * direction * data_speed;
            line_renderer.SetPosition(1, end);

            if((end - original_position).magnitude < 0.5f)
            {
                done = true;
                line_renderer.SetPosition(1, original_position);
            }

            yield return null;
        }
    }
}
