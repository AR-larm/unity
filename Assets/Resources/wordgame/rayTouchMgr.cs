using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayTouchMgr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            // 터치 받아오기
            Touch touch = Input.GetTouch(0); 
            Vector3 TouchPosition = new Vector3(touch.position.x, touch.position.y, 100);
            // ray!
            Ray ray = Camera.main.ScreenPointToRay(TouchPosition);
            RaycastHit hit;

            // 누른 오브젝트 태그 확인
            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                if (hit.collider.tag == "alphabetObject"){
                    alphabetMgr hitAlp = hit.transform.gameObject.GetComponentInParent<alphabetMgr>();
                    gameMgr.selectedAlp = hitAlp;
                } else if (hit.collider.tag == "blankObject"){
                    blankMgr hitBlank = hit.transform.gameObject.GetComponent<blankMgr>();
                    gameMgr.touchedBlank = hitBlank;
                    gameMgr.touchedBlankValue = hitBlank.blankValue;
                }
            } 
           

            
        }        
    }
}
