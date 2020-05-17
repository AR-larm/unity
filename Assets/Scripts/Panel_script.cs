using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_script : MonoBehaviour
{
    public static GameObject panel;
    public static bool panelOn;
    // Start is called before the first frame update
    void Start()
    {
        panel = GameObject.Find("Panel");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
