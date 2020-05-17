using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class time_script : MonoBehaviour
{
    private Text current_time;
    // Start is called before the first frame update
    void Start()
    {
        current_time = GameObject.Find("time").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        current_time.text = DateTime.Now.ToString("hh:mm tt").ToLower();
    }
}
