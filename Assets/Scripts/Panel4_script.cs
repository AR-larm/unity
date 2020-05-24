using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class Panel4_script : MonoBehaviour
{
    public static GameObject light_panel;

    private GameObject lightalert;
    // Start is called before the first frame update
    void Start()
    {
        light_panel = GameObject.Find("Panel4");
        lightalert = GameObject.Find("LightAlert");
        //lightalert.SetActive(false);
        //light_panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Frame.LightEstimate.PixelIntensity <= 0.2)
        {
            //light_panel.SetActive(true);
            lightalert.SetActive(true);
        }
        else
        {
            //light_panel.SetActive(false);
            lightalert.SetActive(false);
        }
        
        
    }
}
