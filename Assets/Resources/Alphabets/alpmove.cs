using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alpmove : MonoBehaviour
{
    float tC = 0; // timeCount
    Vector3 basePoint; // 영단어 캐릭터의 위치 기록, 이후 이동 시 기준 위치가 된다!

    // Start is called before the first frame update
    void Start()
    {
        basePoint = new Vector3 (transform.position.x , transform.position.y, transform.position.z);
        
    }

    // Update is called once per frame
    void Update()
    {
        tC += Time.deltaTime;



        transform.localPosition = new Vector3 (Mathf.Cos(tC)/5, 0.0f, Mathf.Sin(tC)/5);
        
        transform.LookAt(basePoint);
        
        transform.Rotate(new Vector3(0, 180, 0));
    
        
    }
}
