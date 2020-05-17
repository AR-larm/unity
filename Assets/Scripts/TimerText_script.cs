using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System;
using GoogleARCore.Examples.Common;

public class TimerText_script : MonoBehaviour
{
    Stopwatch stopwatch = new Stopwatch();
    private Text time_left;

    public static bool panelOn = false;

    // Start is called before the first frame update
    void Start()
    {
        time_left = GameObject.Find("TimeLeft").GetComponent<Text>();
        stopwatch.Start();
        //GameObject.Find("Panel").SetActive(false);
        Panel_script.panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Double t_left_second = 120 - stopwatch.ElapsedMilliseconds / 1000f;

        //Double t_left_second = 10 - stopwatch.ElapsedMilliseconds / 1000f;

        if (t_left_second <= 0)
        {
            Panel_script.panel.SetActive(true);
            
            GameObject.Find("PointCloud").GetComponent<PointcloudVisualizer>().enabled = false;
            GameObject.Find("PlaneVisualizer").GetComponent<Detect_Plane_script>().enabled = false;
            GameObject.Find("MunMunTouchManager").GetComponent<MunMunTouchMgr_script>().enabled = false;
            

            //Application.Quit();
        }
        else
        {
            time_left.text = "남은 시간: " + String.Format("{0:00}:{1:00}", (int)t_left_second/60, (int)t_left_second%60);
        }
        
        
    }
}
