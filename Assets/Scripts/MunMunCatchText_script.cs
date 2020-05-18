using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MunMunCatchText_script : MonoBehaviour
{
    private Text myScore;
    // Start is called before the first frame update
    void Start()
    {
        myScore = GameObject.Find("CatchScore").GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        myScore.text = "잡은 먼먼이 개수: " + MunMunTouchMgr_script.munmun_catch_count;


        //먼먼이 잡는 횟수 3회시 게임 종료
        if(MunMunTouchMgr_script.munmun_catch_count == 3)
        {
            StartCoroutine(ExampleCoroutine3());
        }
    }
    IEnumerator ExampleCoroutine3()
    {
        yield return new WaitForSeconds(2f);
        Application.Quit();
    }
}
