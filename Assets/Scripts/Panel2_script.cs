using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel2_script : MonoBehaviour
{
    public static GameObject success_panel;

    // Start is called before the first frame update
    void Start()
    {
        success_panel = GameObject.Find("Panel2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
