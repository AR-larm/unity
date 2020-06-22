using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameMgr : MonoBehaviour
{

    // var word
    private string[] wordList = 
    {
        "EXCLUSIVE",
        "ACCURATE",
        "SUBSEQUENT",
        "CONSTRAINT",
        "ESTABLISH"
    };
    private string[] wordmeanList = 
    {
        "독점적인, 배타적인", 
        "정확한, 정밀한", 
        "다음의, 차후의",
        "제약, 통제",
        "설립하다, 수립하다"
    };
    
    [Header("gameMgr : 게임 전반 진행 관리")]
    public string word;
    private string wordMeaning;
    private int wordIndex;
    private int wordLength;

    // var blank
    private string wordLeft;
    private string wordBlank;
    private string wordRight;

    // var wordObject
    private GameObject wordUIBox;
    private Text wordUILeft;
    private GameObject wordUIBlanks;
    private blankMgr wordUIBlank1;
    private blankMgr wordUIBlank2;
    private blankMgr wordUIBlank3;
    private Text wordUIRight;
    private Text wordUIMeaning;

    // var textures
    /*
    [SerializeField] public Texture alptex_A;
    [SerializeField] public Texture alpemi_A;
    [SerializeField] public Texture alptex_B;
    [SerializeField] public Texture alpemi_B;
    [SerializeField] public Texture alptex_C;
    [SerializeField] public Texture alpemi_C;
    [SerializeField] public Texture alptex_D;
    [SerializeField] public Texture alpemi_D;
    [SerializeField] public Texture alptex_E;
    [SerializeField] public Texture alpemi_E;
    [SerializeField] public Texture alptex_F;
    [SerializeField] public Texture alpemi_F;

    [SerializeField] public Texture alptex_G;
    [SerializeField] public Texture alpemi_G;
    [SerializeField] public Texture alptex_H;
    [SerializeField] public Texture alpemi_H;
    [SerializeField] public Texture alptex_I;
    [SerializeField] public Texture alpemi_I;
    [SerializeField] public Texture alptex_J;
    [SerializeField] public Texture alpemi_J;
    [SerializeField] public Texture alptex_K;
    [SerializeField] public Texture alpemi_K;
    [SerializeField] public Texture alptex_L;
    [SerializeField] public Texture alpemi_L;

    [SerializeField] public Texture alptex_M;
    [SerializeField] public Texture alpemi_M;
    [SerializeField] public Texture alptex_N;
    [SerializeField] public Texture alpemi_N;
    [SerializeField] public Texture alptex_O;
    [SerializeField] public Texture alpemi_O;
    [SerializeField] public Texture alptex_P;
    [SerializeField] public Texture alpemi_P;
    [SerializeField] public Texture alptex_Q;
    [SerializeField] public Texture alpemi_Q;
    [SerializeField] public Texture alptex_R;
    [SerializeField] public Texture alpemi_R;

    [SerializeField] public Texture alptex_S;
    [SerializeField] public Texture alpemi_S;
    [SerializeField] public Texture alptex_T;
    [SerializeField] public Texture alpemi_T;
    [SerializeField] public Texture alptex_U;
    [SerializeField] public Texture alpemi_U;
    [SerializeField] public Texture alptex_V;
    [SerializeField] public Texture alpemi_V;
    [SerializeField] public Texture alptex_W;
    [SerializeField] public Texture alpemi_W;
    [SerializeField] public Texture alptex_X;
    [SerializeField] public Texture alpemi_X;
    [SerializeField] public Texture alptex_Y;
    [SerializeField] public Texture alpemi_Y;
    [SerializeField] public Texture alptex_Z;
    [SerializeField] public Texture alpemi_Z;
    */

    // var alphabetObjects
    public char[] answerList;
    private alphabetMgr alp1; 
    private alphabetMgr alp2;
    private alphabetMgr alp3;
    private alphabetMgr alp4;
    private alphabetMgr alp5;
    private alphabetMgr alp6;

    public static alphabetMgr selectedAlp;
    public char crntSelectedAlpValue;

    public static blankMgr touchedBlank;
    public static char touchedBlankValue;

    // var public flags
    public int AnsweredCount = 0;

    // var private flags
    private bool isGameStart = false;


    IEnumerator gameplayer;


    // Start is called before the first frame update
    void Start()
    {
        /* 문제 단어 설정 */
        // 랜덤 단어 설정
        wordIndex = Random.Range(0,wordList.Length);
        word = wordList[wordIndex];
        wordMeaning = wordmeanList[wordIndex];
        wordLength = word.Length;
        Debug.Log("word : "+word+" / "+wordLength+" / "+wordMeaning);

        // blank 랜덤설정
        int blankStart = Random.Range(0, wordLength-3);
        Debug.Log("blank : "+blankStart+"~"+(blankStart+3));

        wordLeft = word.Substring(0, blankStart);
        wordBlank = word.Substring(blankStart, 3);
        wordRight = word.Substring(blankStart+3);
        Debug.Log("parseword : "+wordLeft+"/"+wordBlank+"/"+wordRight);

        // 게임오브젝트 찾기
        // 단어관련
        wordUIBox = GameObject.Find("wordCanvas/word/wordBox");
        wordUILeft = GameObject.Find("wordCanvas/word/wordBox/wordLeft").GetComponent<Text>();
        wordUIBlanks = GameObject.Find("wordCanvas/word/wordBox/blanks");
        wordUIBlank1 = GameObject.Find("wordCanvas/word/wordBox/blanks/blank1").GetComponent<blankMgr>();
        wordUIBlank2 = GameObject.Find("wordCanvas/word/wordBox/blanks/blank2").GetComponent<blankMgr>();
        wordUIBlank3 = GameObject.Find("wordCanvas/word/wordBox/blanks/blank3").GetComponent<blankMgr>();
        wordUIRight = GameObject.Find("wordCanvas/word/wordBox/wordRight").GetComponent<Text>();
        wordUIMeaning = GameObject.Find("wordCanvas/meaning").GetComponent<Text>();
        // 알파벳오브젝트 관련
        alp1 = GameObject.Find("alphabets/alp1/alpmodel").GetComponent<alphabetMgr>();
        alp2 = GameObject.Find("alphabets/alp2/alpmodel").GetComponent<alphabetMgr>();
        alp3 = GameObject.Find("alphabets/alp3/alpmodel").GetComponent<alphabetMgr>();
        alp4 = GameObject.Find("alphabets/alp4/alpmodel").GetComponent<alphabetMgr>();
        alp5 = GameObject.Find("alphabets/alp5/alpmodel").GetComponent<alphabetMgr>();
        alp6 = GameObject.Find("alphabets/alp6/alpmodel").GetComponent<alphabetMgr>();

        // 단어 ui 설정
        wordUILeft.text = wordLeft;
        wordUIRight.text = wordRight;
        wordUIMeaning.text = wordMeaning;

        // 단어 위치 재조정
        float widthL = wordUILeft.gameObject.GetComponent<Text>().preferredWidth / 1000;
        float widthB = wordUIBlanks.gameObject.GetComponent<RectTransform>().rect.width;
        float widthR = wordUIRight.gameObject.GetComponent<Text>().preferredWidth / 1000;
        wordUIBox.GetComponent<RectTransform>().sizeDelta = new Vector2(widthL+widthB+widthR, 0.25f);

        wordUILeft.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,0,0);
        wordUIBlanks.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(widthL,0,0);
        wordUIRight.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(widthL+widthB,0,0);


        // 빈칸 오브젝트에 값 설정
        wordUIBlank1.setBlankValue(wordBlank[0]);
        wordUIBlank2.setBlankValue(wordBlank[1]);
        wordUIBlank3.setBlankValue(wordBlank[2]);

        /* 알파벳 텍스처 찾기*/
        /*
        alptex_A = Resources.Load<Texture>("wordgame/alptex/alptex_A");
        alpemi_A = Resources.Load<Texture>("wordgame/alptex/alptex_Aemi");
        alptex_B = Resources.Load<Texture>("wordgame/alptex/alptex_B");
        alpemi_B = Resources.Load<Texture>("wordgame/alptex/alptex_Bemi");
        alptex_C = Resources.Load<Texture>("wordgame/alptex/alptex_C");
        alpemi_C = Resources.Load<Texture>("wordgame/alptex/alptex_Cemi");
        alptex_D = Resources.Load<Texture>("wordgame/alptex/alptex_D");
        alpemi_D = Resources.Load<Texture>("wordgame/alptex/alptex_Demi");
        alptex_E = Resources.Load<Texture>("wordgame/alptex/alptex_E");
        alpemi_E = Resources.Load<Texture>("wordgame/alptex/alptex_Eemi");
        alptex_F = Resources.Load<Texture>("wordgame/alptex/alptex_F");
        alpemi_F = Resources.Load<Texture>("wordgame/alptex/alptex_Femi");
        
        alptex_G = Resources.Load<Texture>("wordgame/alptex/alptex_G");
        alpemi_G = Resources.Load<Texture>("wordgame/alptex/alptex_Gemi");
        alptex_H = Resources.Load<Texture>("wordgame/alptex/alptex_H");
        alpemi_H = Resources.Load<Texture>("wordgame/alptex/alptex_Hemi");
        alptex_I = Resources.Load<Texture>("wordgame/alptex/alptex_I");
        alpemi_I = Resources.Load<Texture>("wordgame/alptex/alptex_Iemi");
        alptex_J = Resources.Load<Texture>("wordgame/alptex/alptex_J");
        alpemi_J = Resources.Load<Texture>("wordgame/alptex/alptex_Jemi");
        alptex_K = Resources.Load<Texture>("wordgame/alptex/alptex_K");
        alpemi_K = Resources.Load<Texture>("wordgame/alptex/alptex_Kemi");
        alptex_L = Resources.Load<Texture>("wordgame/alptex/alptex_L");
        alpemi_L = Resources.Load<Texture>("wordgame/alptex/alptex_Lemi");
    
        alptex_M = Resources.Load<Texture>("wordgame/alptex/alptex_M");
        alpemi_M = Resources.Load<Texture>("wordgame/alptex/alptex_Memi");
        alptex_N = Resources.Load<Texture>("wordgame/alptex/alptex_N");
        alpemi_N = Resources.Load<Texture>("wordgame/alptex/alptex_Nemi");
        alptex_O = Resources.Load<Texture>("wordgame/alptex/alptex_O");
        alpemi_O = Resources.Load<Texture>("wordgame/alptex/alptex_Oemi");
        alptex_P = Resources.Load<Texture>("wordgame/alptex/alptex_P");
        alpemi_P = Resources.Load<Texture>("wordgame/alptex/alptex_Pemi");
        alptex_Q = Resources.Load<Texture>("wordgame/alptex/alptex_Q");
        alpemi_Q = Resources.Load<Texture>("wordgame/alptex/alptex_Qemi");
        alptex_R = Resources.Load<Texture>("wordgame/alptex/alptex_R");
        alpemi_R = Resources.Load<Texture>("wordgame/alptex/alptex_Remi");

        alptex_S = Resources.Load<Texture>("wordgame/alptex/alptex_S");
        alpemi_S = Resources.Load<Texture>("wordgame/alptex/alptex_Semi");
        alptex_T = Resources.Load<Texture>("wordgame/alptex/alptex_T");
        alpemi_T = Resources.Load<Texture>("wordgame/alptex/alptex_Temi");
        alptex_U = Resources.Load<Texture>("wordgame/alptex/alptex_U");
        alpemi_U = Resources.Load<Texture>("wordgame/alptex/alptex_Uemi");
        alptex_V = Resources.Load<Texture>("wordgame/alptex/alptex_V");
        alpemi_V = Resources.Load<Texture>("wordgame/alptex/alptex_Vemi");
        alptex_W = Resources.Load<Texture>("wordgame/alptex/alptex_W");
        alpemi_W = Resources.Load<Texture>("wordgame/alptex/alptex_Wemi");
        alptex_X = Resources.Load<Texture>("wordgame/alptex/alptex_X");
        alpemi_X = Resources.Load<Texture>("wordgame/alptex/alptex_Xemi");
        alptex_Y = Resources.Load<Texture>("wordgame/alptex/alptex_Y");
        alpemi_Y = Resources.Load<Texture>("wordgame/alptex/alptex_Yemi");
        alptex_Z = Resources.Load<Texture>("wordgame/alptex/alptex_Z");
        alpemi_Z = Resources.Load<Texture>("wordgame/alptex/alptex_Zemi");
        */

        /* 알파벳 오브젝트 세팅*/
        answerList = new char[6];
        answerList[0] = wordBlank[0];
        answerList[1] = wordBlank[1];
        answerList[2] = wordBlank[2];
        answerList[3] = '0';
        answerList[4] = '0';
        answerList[5] = '0';

        char[] tmpAlps = {'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};
        int tmpPoint = 3;

        // 정답외 랜덤알파벳
        while (tmpPoint < answerList.Length){
            int randAlp = Random.Range(0, tmpAlps.Length);

            bool randIsNotIn = true;
            foreach (char C in answerList){
                if (C == tmpAlps[randAlp]){
                    randIsNotIn = false;
                }
            }

            if (randIsNotIn){
                answerList[tmpPoint] = tmpAlps[randAlp];
                tmpPoint++;
            }
        }
        Debug.Log("ansList notshuff : "+answerList[0]+","+answerList[1]+","+answerList[2]+","+answerList[3]+","+answerList[4]+","+answerList[5]);

        // 알파벳 어레이 셔플
        for (int i = 0 ; i<answerList.Length ; i++){
            int ranI = Random.Range(0, answerList.Length);
            char tmpC = answerList[ranI];
            answerList[ranI] = answerList[i];
            answerList[i] = tmpC;
        }

        Debug.Log("ansList shuffled : "+answerList[0]+","+answerList[1]+","+answerList[2]+","+answerList[3]+","+answerList[4]+","+answerList[5]);

        // 알파벳 오브젝트 세팅
        setAlpObject(alp1, answerList[0]);
        setAlpObject(alp2, answerList[1]);
        setAlpObject(alp3, answerList[2]);
        setAlpObject(alp4, answerList[3]);
        setAlpObject(alp5, answerList[4]);
        setAlpObject(alp6, answerList[5]);

        // 기본세팅끝!
       

       

    }

    // Update is called once per frame
    void Update()
    {
        // 세팅끝났을시 최초1회실행
        if (timeMgr.TimeState==timeMgr.State.playGame && !isGameStart){
            isGameStart = true;
            gameplayer = PlayGame();
            StartCoroutine(gameplayer);
        }
    }

    void setAlpObject(alphabetMgr alpObj, char C){
        Texture alptex = Resources.Load<Texture>("wordgame/alptex/alptex_"+C);
        Texture alpemi = Resources.Load<Texture>("wordgame/alptex/alptex_"+C+"emi");

        alpObj.alphabetValue = C;
        alpObj.setTexture(alptex, alpemi);
    }


    IEnumerator PlayGame(){
        crntSelectedAlpValue = '0';
        touchedBlankValue = '0';
        AnsweredCount = 0;

        while (true){
            yield return new WaitForSeconds(0.03f);
            // selectedAlp 없을때 값도 삭제
            if (selectedAlp == null){
                crntSelectedAlpValue = '0';
            }
            // selectAlp 존재하고 + selectedAlp 바뀌었을 때 
            // -> crnt값 수정, selectAlp상태 wait로 수정 
            if (selectedAlp != null && crntSelectedAlpValue != selectedAlp.alphabetValue){
                Debug.Log("gameMgr : selectedAlp changed");
                crntSelectedAlpValue = selectedAlp.alphabetValue;
                selectedAlp.alpStatus = alphabetMgr.Status.wait;
            }
            // selectAlp 존재하고 + 5초 지나서 selectedAlp의 status가 wait으로 돌아갔을때
            // -> selectAlp 값 삭제, crnt값 초기화
            if (selectedAlp != null && selectedAlp.alpStatus == alphabetMgr.Status.move1){
                Debug.Log("gameMgr : selectedAlp timeup");
                selectedAlp = null;
                crntSelectedAlpValue = '0';
            }
            // selectAlp 존재하고 + touchBlank 존재하고 
            if (selectedAlp != null && touchedBlank != null ){
                touchedBlankValue = touchedBlank.blankValue;
                if (crntSelectedAlpValue == touchedBlankValue){
                    // 값이 같을 때 : 정답
                    // -> 빈칸 알파벳 보이도록 하고, selectedAlp destoy
                    Debug.Log("gameMgr : correct answer!");
                    touchedBlank.setBlankText();
                    touchedBlank.isAnswered = true;
                    selectedAlp.playBigParticle();
                    yield return new WaitForSeconds(0.2f);
                    Destroy(selectedAlp.gameObject);
                    selectedAlp = null;
                    AnsweredCount++;

                } else {
                    // 값이 다를 때 : 오답
                    // -> 그냥 둠
                    Debug.Log("gameMgr : wrong answer!");

                }
                touchedBlank = null;
                touchedBlankValue = '0'; // 값초기화
            }
            // 정답 3회 맞췄을 경우
            if (AnsweredCount >= 3){
                Debug.Log("gameMgr : end!");
                timeMgr.TimeState = timeMgr.State.gameEnd_success;
                yield return new WaitForSeconds(5f);
                Application.Quit();
            }

        }

    }
}
