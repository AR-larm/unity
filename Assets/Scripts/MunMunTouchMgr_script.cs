using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using System.IO;

public class MunMunTouchMgr_script : MonoBehaviour
{
    private Camera ARCamera;    //ARCore 카메라
    public GameObject placeObject; //터치 시 평면에 생성할 프리팹

    // Start is called before the first frame update
    void Start()
    {
        //ARCore Device 프리팹 하위에 있는 카메라를 찾아서 변수에 할당
        ARCamera = GameObject.Find("First Person Camera").GetComponent<Camera>();

        placeObject.transform.localScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
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
                            GameObject obj = Instantiate(placeObject, hit.Pose.position, hit.Pose.rotation);
                            obj.transform.Rotate(0, 0, 0, Space.Self);

                            var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                            obj.transform.parent = anchor.transform;
                        }
                    }
                    
                }
            }
        }







        /*
        if (Input.touchCount == 0)  return;

        //첫 번재 터치 정보 추출
        Touch touch = Input.GetTouch(0);

        //ARCore에서 제공하는 RaycastHit와 유사한 구조체
        TrackableHit hit;

        //검출 대상을 평면 또는 Feature Point로 한정
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.FeaturePointWithSurfaceNormal;

        //터치한 지점으로 레이 발사
        if(touch.phase == TouchPhase.Began && Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
        {
            placeObject.transform.localScale = new Vector3(1, 1, 1);
            //객체를 고정할 앵커를 설정
            var anchor = hit.Trackable.CreateAnchor(hit.Pose);
            //객체를 생성
            GameObject obj = Instantiate(placeObject, hit.Pose.position, Quaternion.identity, anchor.transform);

            //생성한 객체가 사용자 쪽을 바라보도록 회전값 계산
            var rot = Quaternion.LookRotation(ARCamera.transform.position - hit.Pose.position);

            //사용자 쪽 회전값 적용
            obj.transform.rotation = Quaternion.Euler(ARCamera.transform.position.x, rot.eulerAngles.y, ARCamera.transform.position.z);
        }
        */

    }
}
