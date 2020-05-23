using GoogleARCore.Examples.Common;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class startbutton_script : MonoBehaviour
{
    public static Stopwatch stopwatch;
    public Button StartButton;

    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener(StartBtn_OnClick);
    }

    void StartBtn_OnClick()
    {
        GameObject.Find("PointCloud").GetComponent<PointcloudVisualizer>().enabled = true;
        GameObject.Find("PlaneVisualizer").GetComponent<Detect_Plane_script>().enabled = true;
        GameObject.Find("MunMunTouchManager").GetComponent<MunMunTouchMgr_script>().enabled = true;
        GameObject.Find("TimeLeft").GetComponent<TimerText_script>().enabled = true;

        Panel3_script.start_panel.SetActive(false);

        stopwatch = new Stopwatch();
        stopwatch.Start();
    }
}
