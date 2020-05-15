using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using System.IO;
using System.Security.Cryptography;

public class MunMunTouchMgr_script : MonoBehaviour
{
    private Camera ARCamera;    //ARCore 카메라

    public static int munmun_catch_count;        //먼먼이 잡은 개수

    public GameObject broomstick;

    // Start is called before the first frame update
    void Start()
    {
        //ARCore Device 프리팹 하위에 있는 카메라를 찾아서 변수에 할당
        ARCamera = GameObject.Find("First Person Camera").GetComponent<Camera>();

        munmun_catch_count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        int prev_catch_count = munmun_catch_count;
        Debug.Log("Camera Position: " + ARCamera.transform.position);
        if (Detect_Plane_script.munmuns.Count != 0)
        {
            for (int i = 0; i < Detect_Plane_script.munmuns.Count; i++)
            {
                Debug.Log("Position of MunMun Num " + i + ": " + Detect_Plane_script.munmuns[i].transform.localPosition);
                }
        }
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        //ARCore에서 제공하는 RaycastHit와 유사한 구조체
        TrackableHit hit;

        //검출 대상을 평면 또는 Feature Point로 한정
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.FeaturePointWithSurfaceNormal;

        if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
        {
            if ((hit.Trackable is DetectedPlane) && Vector3.Dot(ARCamera.transform.position - hit.Pose.position, hit.Pose.rotation * Vector3.up) < 0)
            {
                Debug.Log("Hit at back of the current DetectedPlane");
            }
            else
            {
                if (hit.Trackable is DetectedPlane)
                {
                    Debug.Log("Hit Position: " + hit.Pose.position);
                    for (int i=0;i<Detect_Plane_script.munmuns.Count; i++)
                    {

                        //hit position과 먼먼이 position 비교. 오차 범위 내에 먼먼이 위치시에 동작.
                        if((hit.Pose.position.x-0.03f<=Detect_Plane_script.munmuns[i].transform.position.x &&hit.Pose.position.x+0.02f>Detect_Plane_script.munmuns[i].transform.position.x)
                            && (hit.Pose.position.y - 0.03f < Detect_Plane_script.munmuns[i].transform.position.y && hit.Pose.position.y + 0.07f > Detect_Plane_script.munmuns[i].transform.position.y)
                            && (hit.Pose.position.z - 0.1f < Detect_Plane_script.munmuns[i].transform.position.z && hit.Pose.position.z + 0.02f > Detect_Plane_script.munmuns[i].transform.position.z))
                        {
                            //먼먼이 터치시 mun_clean state로 바꿈
                            Animator mun_animator = Detect_Plane_script.munmuns[i].GetComponent<Animator>();
                            mun_animator.applyRootMotion = true;
                            mun_animator.runtimeAnimatorController = Resources.Load("0501mun/mun_anim") as RuntimeAnimatorController;
                            mun_animator.SetBool("isClean", true);

                            if (Detect_Plane_script.munmuns_activationFlag[i] == true)
                            {
                                //빗자루 생성
                                GameObject obj = Instantiate(broomstick, Detect_Plane_script.munmuns[i].transform.position, Detect_Plane_script.munmuns[i].transform.rotation);
                                obj.SetActive(true);

                                //빗자루 위치를 먼먼이 위로 세팅
                                var b_pos = obj.transform.position;
                                b_pos.y = b_pos.y + 0.14f;
                                obj.transform.position = b_pos;

                                obj.transform.Rotate(0, 0, 0, Space.Self);

                                Animator broom_animatpr = broomstick.GetComponent<Animator>();
                                broom_animatpr.applyRootMotion = true;
                                broom_animatpr.runtimeAnimatorController = Resources.Load("0515/0501broom/broom_anim") as RuntimeAnimatorController;
                                broom_animatpr.SetBool("isClean", true);

                                Detect_Plane_script.munmuns_activationFlag[i] = false;

                                //먼먼이 잡은 횟수 증가
                                munmun_catch_count++;

                                StartCoroutine(ExampleCoroutine2(obj));
                            }
                            
                            StartCoroutine(ExampleCoroutine(i));
                            
                            break;
                        }
                    }
                    
                }
            }
        }
    }

    //먼먼이 오브젝트 삭제
    void MunMun_Delete(int position)
    {
        Detect_Plane_script.munmuns[position].SetActive(false);
        Detect_Plane_script.munmuns.RemoveAt(position);
    }

    //먼먼이가 위치한 평면 삭제
    void MunMun_DetectedPlane_Delete(int position)
    {
        Detect_Plane_script.planes[position].SetActive(false);
        Detect_Plane_script.planes.RemoveAt(position);
    }

    void MunMun_ActiveFlag_Delet(int position)
    {
        Detect_Plane_script.munmuns_activationFlag.RemoveAt(position);
    }

    //먼먼이 2초 뒤 삭제 진행
    IEnumerator ExampleCoroutine(int position)
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2f);
        MunMun_Delete(position);
        MunMun_DetectedPlane_Delete(position);
        MunMun_ActiveFlag_Delet(position);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    IEnumerator ExampleCoroutine2(GameObject b_obj)
    {
        yield return new WaitForSeconds(2f);
        b_obj.SetActive(false);
        //Destroy(b_obj);
    }
}
