using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityStatus : MonoBehaviour
{
    public Sprite sprite_default;
    public Sprite sprite_true;
    public Sprite sprite_false;

    bool disturbed = false;

    public void SetStatus(bool status)
    {
        if(!disturbed)
        {
            SpriteRenderer sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
            
            if(status)
            {
                sprite_renderer.sprite = sprite_true;
                // sprite_renderer.color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
            }
            else
            {
                sprite_renderer.sprite = sprite_false;
                // sprite_renderer.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            }
            disturbed = true;
        }
    }

    public void Reset()
    {
        disturbed = false;
        SpriteRenderer sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
        sprite_renderer.sprite = sprite_default;
        // sprite_renderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }
}
