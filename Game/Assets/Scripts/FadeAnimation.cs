using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeAnimation : MonoBehaviour
{
    UIType UI_type;

    void Start()
    {
        if(gameObject.GetComponent<Button>() != null)
        {
            UI_type = UIType.Button;
            gameObject.GetComponent<Button>().interactable = false;

            Image image = gameObject.GetComponent<Image>();
            Color color = image.color;
            color.a = 0.0f;
            image.color = color;
        }
        else if(gameObject.GetComponent<Text>() != null)
        {
            UI_type = UIType.Text;
            
            Text text = gameObject.GetComponent<Text>();
            Color color = text.color;
            color.a = 0.0f;
            text.color = color;
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
                    if(color.a >= 1)
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
                    if(color.a >= 1)
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
        Text
    }
}
