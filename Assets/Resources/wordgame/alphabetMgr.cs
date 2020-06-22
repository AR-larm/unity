using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alphabetMgr : MonoBehaviour
{

    [Header("alphabetMgr : 알파벳 상태 관리")]
    
    // 이 오브젝트가 가진 알파벳 값
    public char alphabetValue;

    // 상태
    public enum Status 
    {
        move1, 
        move2, 
        wait
    };
    public Status alpStatus;

    // 포함 오브젝트
    /* 애니메이션에서 컨트롤
    private GameObject face_move;
    private GameObject face_wait;
    private GameObject waitsign;
    */
    private Transform alpWrapper;
    public float alpSpeed = -0.6f; // defaultvalue
    //public Vector3 alpRotation;

    // 컴포넌트
    private Animator alpAnim;
    //private Renderer alpRender; // 그냥 에셋의 매터리얼 직접 접근해서 수정하는걸로
    public Material alpMaterial;

    private ParticleSystem parti_small;
    private ParticleSystem parti_big;



    // var flags
    bool isAlpStart = false;
   



    // Start is called before the first frame update
    void Start()
    {
        alpStatus = Status.move1;   // 1,3,5
        //alpStatus = Status.move2;   // 2,4,6

        alpWrapper = transform.parent.gameObject.transform;
        alpAnim = transform.GetComponent<Animator>();

        parti_small = transform.Find("alpparti_small").GetComponent<ParticleSystem>();
        parti_big = transform.Find("alpparti_big").GetComponent<ParticleSystem>();
  
    }

    // Update is called once per frame
    void Update()
    {
        if (timeMgr.TimeState == timeMgr.State.playGame && !isAlpStart){
            isAlpStart = true;
            StartCoroutine(setAlpModel());
        }
    }

    // coroutine : status 따라서 alpmodel 세팅
    IEnumerator setAlpModel(){
        Status currentStatus = Status.move1;

        while(true){
            yield return new WaitForSeconds(0.03f);

            if (alpStatus == Status.move1){
                if(currentStatus != Status.move1){
                    // move1로 전환된 경우
                    currentStatus = Status.move1;
                    alpAnim.SetBool("isMove", true);
                }
                alpWrapper.Rotate(new Vector3(alpSpeed,0,0));
            } else if (alpStatus == Status.move2){
                // not enabled now
                //alpWrapper.Rotate(new Vector3(-0.6f,0,0));
            } else if (alpStatus == Status.wait){
                // wait로 전환된 경우
                if(currentStatus != Status.wait){
                    currentStatus = Status.wait;
                    alpAnim.SetBool("isMove", false);
                    StartCoroutine(waitTimer());
                }
            }

        }
    }

    IEnumerator move1corout(){
        while (true){
            yield return new WaitForSeconds(0.03f);
            
        }
    }

    IEnumerator waitTimer(){
        // 일정시간후 다시 움직이도록
        yield return new WaitForSeconds(10.0f);
        if (alpStatus == Status.wait){
            alpStatus = Status.move1;
        }
    }

    public void setTexture(Texture alptex, Texture alpemi){
        alpMaterial.SetTexture("_MainTex", alptex);
        alpMaterial.SetTexture("_EmissionMap", alptex);
    }

    public void playSmallParticle(){
        parti_small.Play();
    }

    public void playBigParticle(){
        parti_big.Play();
    }
}
