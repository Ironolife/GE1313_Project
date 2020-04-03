using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    bool is_frozen = false;
    
    Vector3 mouse_offset;
    float mouse_position_z;

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
        if(!is_frozen)
        {
            transform.parent.position = GetMouseAsWorldPoint() + mouse_offset - (transform.position - transform.parent.position);

            Vector3 position = transform.parent.position;
            position.z += 2f;
            transform.parent.Find("Data").GetComponent<LineRenderer>().SetPositions(new Vector3[]{position, position});
        }
    }

    public void Toggle()
    {
        is_frozen = !is_frozen;
    }
}
