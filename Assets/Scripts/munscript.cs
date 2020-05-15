using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class munscript : MonoBehaviour
{
    [SerializeField] ParticleSystem munparti1;
    [SerializeField] ParticleSystem munparti2;
    [SerializeField] ParticleSystem munparti3;

    Animator mun_anim;
    bool isClean;

    bool isParticleOn;

    // Start is called before the first frame update
    void Start()
    {
        mun_anim = gameObject.GetComponent<Animator>();
        isClean = false;
        isParticleOn = false;

       
    }

    // Update is called once per frame
    void Update()
    {
        if (mun_anim.GetBool("isClean") && !isParticleOn){
            isParticleOn = true;
            StartCoroutine(partiplay());
        }
        
    }

    IEnumerator partiplay(){
        while(mun_anim.GetBool("isClean")){
            munparti1.Play();
            munparti2.Play();
            //munparti3.Play();
            yield return new WaitForSeconds(0.333f);
        }
        isParticleOn = false;
    }
}
