using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System;

public class TimerText_script : MonoBehaviour
{
    Stopwatch stopwatch = new Stopwatch();
    private Text time_left;

    // Start is called before the first frame update
    void Start()
    {
        time_left = GameObject.Find("TimeLeft").GetComponent<Text>();
        stopwatch.Start();
    }

    // Update is called once per frame
    void Update()
    {
        Double t_left_second = 120 - stopwatch.ElapsedMilliseconds / 1000f;

        if(t_left_second <= 0)
        {
            Application.Quit();
        }

        time_left.text = "남은 시간: " + String.Format("{0:00}:{1:00}", (int)t_left_second/60, (int)t_left_second%60);
    }
}
