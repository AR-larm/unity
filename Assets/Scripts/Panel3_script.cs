using GoogleARCore.Examples.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel3_script : MonoBehaviour
{
    public static GameObject start_panel;

    // Start is called before the first frame update
    void Start()
    {
        start_panel = GameObject.Find("Panel3");

        GameObject.Find("PointCloud").GetComponent<PointcloudVisualizer>().enabled = false;
        GameObject.Find("PlaneVisualizer").GetComponent<Detect_Plane_script>().enabled = false;
        GameObject.Find("MunMunTouchManager").GetComponent<MunMunTouchMgr_script>().enabled = false;
        GameObject.Find("TimeLeft").GetComponent<TimerText_script>().enabled = false;
        Panel4_script.light_panel.SetActive(false);

    }
}
