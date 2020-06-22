using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class timeMgr : MonoBehaviour
{
    [Header("timeMgr : 시간 및 상태 관리")]
    public int TimeLimit = 120;
    
    // 상태
    public enum State
    {
        setValue, 
        playGame, 
        gameEnd_fail,
        gameEnd_success
    };
    public static State TimeState;

    // Panel_init
    private GameObject Panel_init;
    private Button StartButton;
    private Text CurrentTime1;

    // Panel_fail
    private GameObject Panel_fail;
    private Text CurrentTime2;
    private Text random_num;
    public InputField random_inputtext;


    // Panel_success
    private GameObject Panel_success;



    // Stopwatch
    private Text TimeLeft;
    public Stopwatch stopwatch;
    public double t_left_second;

    

    // Start is called before the first frame update
    void Start()
    {
        TimeState = State.setValue;

        // Panel_init
        Panel_init = transform.Find("Panel_init").gameObject;
        CurrentTime1 = transform.Find("Panel_init/start_time").GetComponent<Text>();
        StartButton = transform.Find("Panel_init/button/start_Button").GetComponent<Button>();
        StartButton.onClick.AddListener(StartBtn_OnClick);

        // Panel_fail
        Panel_fail = transform.Find("Panel_fail").gameObject;
        CurrentTime2 = transform.Find("Panel_fail/start_time").GetComponent<Text>();
        random_num = transform.Find("Panel_fail/RawImage/randomnum").GetComponent<Text>();
        setRandomVal(random_num);
        random_inputtext = transform.Find("Panel_fail/InputField").GetComponent<InputField>();
        random_inputtext.onEndEdit.AddListener(delegate { LockInput(random_inputtext, random_num); });

        // Panel_success
        Panel_success = transform.Find("Panel_success").gameObject;

        // stopwatch오브젝트 찾기
        TimeLeft = transform.Find("timeLeft").GetComponent<Text>();
        stopwatch = new Stopwatch();

        IEnumerator timechecker = TimeChecker();
        StartCoroutine(timechecker);

        // panel 초기세팅
        Panel_init.SetActive(true);
        Panel_fail.SetActive(false);
        Panel_success.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        t_left_second = TimeLimit - stopwatch.ElapsedMilliseconds / 1000f;
     
    }

    IEnumerator TimeChecker(){
        while(true){
            yield return new WaitForSeconds(0.03f);
            if (TimeState == State.setValue){
                CurrentTime1.text = DateTime.Now.ToString("hh:mm tt").ToLower();
                // start button 눌렀을 때 playgame으로 전환
                // StartBtm_OnClick()
            } else if (TimeState == State.playGame){
                //UnityEngine.Debug.Log("timeMgr : playGame");
                TimeLeft.text = "남은 시간: " + string.Format("{0:00}:{1:00}", (int)t_left_second/60, (int)t_left_second%60);

                if (t_left_second <= 0){
                    TimeState = State.gameEnd_fail;
                }
            } else if (TimeState == State.gameEnd_fail){
                // timeMgr에서 시간 경과시 전환
                Panel_fail.SetActive(true);
                
            } else if (TimeState == State.gameEnd_success){
                // gameMgr에서 3칸 다 맞출시 전환
                Panel_success.SetActive(true);
            }
        }
        
    }

    void StartBtn_OnClick()
    {
        TimeState = State.playGame;      
        Panel_init.SetActive(false);
        stopwatch.Start();
    }

    void setRandomVal(Text textObj){
        // Random objects
        const string NonCapitilizedLetters = "abcdefghijklmnopqrstuvwxyz";
        const string Numbers = "0123456789";
        System.Random random = new System.Random();

        List<string> charSets = new List<string>();
        charSets.Add(NonCapitilizedLetters);
        charSets.Add(Numbers);

        int length = 6;
        StringBuilder sb = new StringBuilder();

        while (length-- > 0)
        {
            int charSet = random.Next(charSets.Count);
            int index = random.Next(charSets[charSet].Length);
            sb.Append(charSets[charSet][index]);
        }

        textObj.text = sb.ToString();
    }

    void LockInput(InputField input, Text textObj)
    {
        if (input.text.Equals(textObj.text))
        {
            Application.Quit();
        }
    }
}
