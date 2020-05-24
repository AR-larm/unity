using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tempText_script : MonoBehaviour
{
    private Text temp_text;
    // Start is called before the first frame update
    void Start()
    {
        temp_text = GameObject.Find("Text4").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //temp_text.text = "앗! 방이 많이 어둡군요!\n먼먼이를 청소하기 위해\n방 불을 켜주세요!\n"+Panel4_script.light;
    }
}
