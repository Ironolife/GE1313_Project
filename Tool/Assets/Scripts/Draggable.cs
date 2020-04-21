using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    Vector3 mouse_offset;
    float mouse_position_z;
    EventControl event_control;

    void Start()
    {
        event_control = GameObject.Find("EventSystem").GetComponent<EventControl>();
    }

    void OnMouseDown()
    {
        mouse_position_z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mouse_offset = gameObject.transform.position - GetMouseAsWorldPoint();
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mouse_point = Input.mousePosition;
        mouse_point.z = mouse_position_z;
        return Camera.main.ScreenToWorldPoint(mouse_point);
    }

    void OnMouseDrag()
    {
        if(event_control.can_drag)
        {
            Vector3 new_position = GetMouseAsWorldPoint() + mouse_offset;
            new_position.x = Mathf.Clamp(new_position.x, -4.05f, 8.5f);
            new_position.y = Mathf.Clamp(new_position.y, -4.6f, 4.6f);
            transform.position = new_position;
        }
    }

}
