using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.Examples.Common;

public class Detect_Plane_script : MonoBehaviour
{
    /// <summary>
    /// A prefab for tracking and visualizing detected planes.
    /// </summary>
    public GameObject DetectedPlanePrefab;

    /// <summary>
    /// A list to hold new planes ARCore began tracking in the current frame. This object is
    /// used across the application to avoid per-frame allocations.
    /// </summary>
    private List<DetectedPlane> m_NewPlanes = new List<DetectedPlane>();

    public GameObject mun;

    //private Anchor anchor;
    public static List<Anchor> munmuns = new List<Anchor>();    //먼먼이 anchor들을 저장. 이후 먼먼이들의 위치 및 터치시의 listener에서 사용 예정.

    private Camera ARCamera;    //ARCore 카메라
    //private int count;

    public void Start()
    {
        ARCamera = GameObject.Find("First Person Camera").GetComponent<Camera>();
        //count = 0;
    }

    /// <summary>
    /// The Unity Update method.
    /// </summary>
    public void Update()
    {
        Anchor anchor;
        // Check that motion tracking is tracking.
        if (Session.Status != SessionStatus.Tracking || ARCamera.transform.rotation.x<0)
        {
            return;
        }
        //Debug.Log("camera rotation " + count + ": " + ARCamera.transform.rotation.x);
        //count++;


        // Iterate over planes found in this frame and instantiate corresponding GameObjects to
        // visualize them.
        Session.GetTrackables<DetectedPlane>(m_NewPlanes, TrackableQueryFilter.New);
        for (int i = 0; i < m_NewPlanes.Count; i++)
        {
            // Instantiate a plane visualization prefab and set it to track the new plane. The
            // transform is set to the origin with an identity rotation since the mesh for our
            // prefab is updated in Unity World coordinates.
            GameObject planeObject =
                Instantiate(DetectedPlanePrefab, Vector3.zero, Quaternion.identity, transform);
            planeObject.GetComponent<DetectedPlaneVisualizer>().Initialize(m_NewPlanes[i]);


            //tracking이 진행중일때 먼먼이를 plane위에 생성.
            if (m_NewPlanes[i].TrackingState == TrackingState.Tracking)
            {
                GameObject obj = Instantiate(mun, new Vector3(m_NewPlanes[i].CenterPose.position.x, m_NewPlanes[i].CenterPose.position.y, m_NewPlanes[i].CenterPose.position.z), Quaternion.identity, transform);
                
                //먼먼이 object의 방향을 카메라가 보는 방향으로 설정
                var direction = Quaternion.LookRotation(obj.gameObject.transform.position - ARCamera.transform.position).eulerAngles;
                direction.x = m_NewPlanes[i].CenterPose.position.x;
                direction.z = m_NewPlanes[i].CenterPose.position.z;
                obj.transform.rotation = Quaternion.Euler(direction);
                
                //먼먼이 object 사이즈 변경
                obj.transform.localScale = new Vector3(1, 1, 1);

                anchor = m_NewPlanes[i].CreateAnchor(m_NewPlanes[i].CenterPose);
                obj.transform.parent = anchor.transform;

                munmuns.Add(anchor);

                //Debug.Log("position: " + anchor.transform.localPosition);
                break;
            }

        }
        //앵커들 위치 이동
        /*
        if (munmuns.Count!=0)
        {
            for(int i = 0; i < munmuns.Count; i++)
            {
                munmuns[i].transform.Translate(Vector3.forward * Time.deltaTime * 0.5f);
                //Debug.Log("position of MunMun Num "+i+": " + munmuns[i].transform.localPosition);

            }
        }
        */
        

        /*
        GameObject obj = Instantiate(mun, Vector3.zero, Quaternion.identity);
        obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        obj.GetComponent<DetectedPlaneVisualizer>().Initialize(m_NewPlanes[0]);
        */
    }
}
