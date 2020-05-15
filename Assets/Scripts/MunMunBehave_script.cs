using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunMunBehave_script : MonoBehaviour
{
    public static Animator mun_anim;
    public static int IsStick;
    public static int IsRoll;
    public static int IsRun;
    public static int IsClean;

    // Start is called before the first frame update
    void Start()
    {
        mun_anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public static void setStick()
    {
        mun_anim.SetBool("isStick", true);
    }
}
