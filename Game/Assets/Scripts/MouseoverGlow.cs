using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseoverGlow : MonoBehaviour
{
    SpriteRenderer sprite_renderer;
    bool status = false;
    public GameObject label;
    Text text;
    public string response_text;
    public bool validity = false;

    void Start()
    {
        sprite_renderer = transform.Find("Glow").GetComponent<SpriteRenderer>();
        text = label.GetComponent<Text>();
    }

    void Update()
    {
        if(status)
        {
            if(sprite_renderer.color.a < 1)
            {
                Color old_color = sprite_renderer.color;
                old_color.a += Time.deltaTime * 3.0f;

                sprite_renderer.color = old_color;
            }
            if(text.color.a < 1)
            {
                Color old_color = text.color;
                old_color.a += Time.deltaTime * 3.0f;

                text.color = old_color;
            }
        }
        else
        {
            if(sprite_renderer.color.a > 0)
            {
                Color old_color = sprite_renderer.color;
                old_color.a -= Time.deltaTime * 3.0f;

                sprite_renderer.color = old_color;
            }
            if(text.color.a > 0)
            {
                Color old_color = text.color;
                old_color.a -= Time.deltaTime * 3.0f;

                text.color = old_color;
            }
        }
    }

    void OnMouseOver()
    {
        status = true;
    }

    void OnMouseExit()
    {
        status = false;
    }

    public void StartFadeOutLabel()
    {
        StartCoroutine(FadeOutLabel());
    }

    IEnumerator FadeOutLabel()
    {
        while(text.color.a > 0)
        {
            Color old_color = text.color;
            old_color.a -= Time.deltaTime * 3.0f;

            text.color = old_color;

            yield return null;
        }
    }

    void OnMouseDown()
    {
        GameObject.Find("EventSystem").GetComponent<EventControl>().P2_Choice(response_text, validity);
        GameObject[] interactables = GameObject.FindGameObjectsWithTag("Interactable");
        foreach (GameObject interactable in interactables)
        {
            interactable.GetComponent<MouseoverGlow>().StartFadeOutLabel();
            interactable.GetComponent<MouseoverGlow>().enabled = false;
        }
    }
}
