using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeAnimation : MonoBehaviour
{
    UIType UI_type;
    public float max_alpha = 1.0f;

    void Start()
    {
        if(gameObject.GetComponent<Text>() != null)
        {
            UI_type = UIType.Text;
            
            Text text = gameObject.GetComponent<Text>();
            Color color = text.color;
            color.a = 0.0f;
            text.color = color;
        }
        else if(gameObject.GetComponent<Button>() != null)
        {
            UI_type = UIType.Button;
            gameObject.GetComponent<Button>().interactable = false;

            Image image = gameObject.GetComponent<Image>();
            Color color = image.color;
            color.a = 0.0f;
            image.color = color;
        }
        else if(gameObject.GetComponent<SpriteRenderer>() != null)
        {
            UI_type = UIType.Object;
            SpriteRenderer sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
            Color color = sprite_renderer.color;
            color.a = 0.0f;
            sprite_renderer.color = color;
        }

        FadeIn();
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(true));
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(false));
    }

    IEnumerator Fade(bool direction)
    {
        bool done = false;

        while(!done)
        {
            if(UI_type == UIType.Button)
            {
                if(direction == false)
                {
                    gameObject.GetComponent<Button>().interactable = false;
                }

                Image image = gameObject.GetComponent<Image>();
                Color color = image.color;
                color.a += (direction? 1.0f: -1.0f) * Time.deltaTime * 2.0f;
                image.color = color;

                if(direction)
                {
                    if(color.a >= max_alpha)
                    {
                        done = true;
                        gameObject.GetComponent<Button>().interactable = true;
                    }
                }
                else
                {
                    if(color.a <= 0)
                    {
                        done = true;
                    }
                }
            } 
            else if(UI_type == UIType.Text)
            {
                Text text = gameObject.GetComponent<Text>();
                Color color = text.color;
                color.a += (direction? 1.0f: -1.0f) * Time.deltaTime * 2.0f;
                text.color = color;

                if(direction)
                {
                    if(color.a >= max_alpha)
                    {
                        done = true;
                    }
                }
                else
                {
                    if(color.a <= 0)
                    {
                        done = true;
                    }
                }
            }
            else if(UI_type == UIType.Object)
            {
                SpriteRenderer sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
                Color color = sprite_renderer.color;
                color.a += (direction? 1.0f: -1.0f) * Time.deltaTime * 2.0f;
                sprite_renderer.color = color;

                if(direction)
                {
                    if(color.a >= max_alpha)
                    {
                        done = true;
                    }
                }
                else
                {
                    if(color.a <= 0)
                    {
                        done = true;
                    }
                }
            }

            yield return null;
        }
    }

    enum UIType
    {
        Button,
        Text,
        Object
    }
}
