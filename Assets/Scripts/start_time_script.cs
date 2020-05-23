using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class start_time_script : MonoBehaviour
{
    private Text current_time2;
    // Start is called before the first frame update
    void Start()
    {
        current_time2 = GameObject.Find("start_time").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        current_time2.text = DateTime.Now.ToString("hh:mm tt").ToLower();
    }
}
