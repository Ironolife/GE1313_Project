using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public GameObject latitude_input;
    public GameObject longitute_input;
    public GameObject zoom_input;
    float latitude;
    float longitute;
    int zoom;
    int width = 384;
    int height = 288;
    int scale = 2;
    string maptype = "hybrid";

    RawImage raw_image;

    void Start()
    {
        raw_image = GetComponent<RawImage>();

        GetParameters();
    }

    public void GetParameters()
    {
        latitude = float.Parse(latitude_input.GetComponent<InputField>().text);
        longitute = float.Parse(longitute_input.GetComponent<InputField>().text);
        zoom = int.Parse(zoom_input.GetComponent<InputField>().text);

        StartCoroutine(GetMap());
    }

    IEnumerator GetMap()
    {
        string url = "https://maps.googleapis.com/maps/api/staticmap?center=" + latitude + "," + longitute + "&zoom=" + zoom + "&size=" + width + "x" + height + "&scale=" + scale +"&maptype=" + maptype + "&key=AIzaSyCxPSngah2m53C-H4RzPzyLa5guct6RE8o";

        WWW www = new WWW(url);
        
        yield return www;

        raw_image.texture = www.texture;
    }
}
