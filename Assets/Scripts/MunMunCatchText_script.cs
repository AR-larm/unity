using GoogleARCore.Examples.Common;
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
        if (MunMunTouchMgr_script.munmun_catch_count == 3)
        {
            StartCoroutine(ExampleCoroutine3());

            GameObject.Find("PointCloud").GetComponent<PointcloudVisualizer>().enabled = false;
            GameObject.Find("PlaneVisualizer").GetComponent<Detect_Plane_script>().enabled = false;
            GameObject.Find("MunMunTouchManager").GetComponent<MunMunTouchMgr_script>().enabled = false;
        }

    }
    IEnumerator ExampleCoroutine3()
    {
        yield return new WaitForSeconds(2f);
        Panel2_script.success_panel.SetActive(true);

        yield return new WaitForSeconds(4f);
        Application.Quit();
    }
}
