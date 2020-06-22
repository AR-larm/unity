using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blankMgr : MonoBehaviour
{

    [Header("blankMgr : 빈칸 관리")]
    public char blankValue;

    public Text answerText;
    public bool isAnswered = false;



    // Start is called before the first frame update
    void Start()
    {
        answerText = transform.Find("answer").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setBlankValue(char CH){
        blankValue = CH;
    }

    public void setBlankText(){
        answerText.text = blankValue+"";
    }
}
