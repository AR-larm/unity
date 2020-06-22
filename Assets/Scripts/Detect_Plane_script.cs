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
    public static List<GameObject> munmuns = new List<GameObject>();    //먼먼이 오브젝트들을 저장. 이후 먼먼이들의 위치 및 터치시의 listener에서 사용 예정.
    public static List<GameObject> planes = new List<GameObject>();     //먼먼이 오브젝트가 위치한 plane들을 저장. 이후 터치시의 listener에서 사용 예정.
    public static List<bool> munmuns_activationFlag = new List<bool>();    //먼먼이 오브젝트들을 저장. 이후 먼먼이들의 위치 및 터치시의 listener에서 사용 예정.

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

                //MunMunAvatarBehave_script.mun_anim1.SetBool("isRoll", true);
                GameObject obj = Instantiate(mun, new Vector3(m_NewPlanes[i].CenterPose.position.x, m_NewPlanes[i].CenterPose.position.y, m_NewPlanes[i].CenterPose.position.z), Quaternion.identity, transform);
                
                //먼먼이 오브젝트를 Stick state로 설정
                Animator animator = obj.GetComponent<Animator>();
                animator.applyRootMotion = true;
                animator.runtimeAnimatorController = Resources.Load("0501mun/mun_anim") as RuntimeAnimatorController;
                animator.SetBool("isStick", true);
                
                Debug.Log("mun position: " + obj.transform.localPosition);
                Debug.Log("Center position: " + m_NewPlanes[i].CenterPose.position);


                //먼먼이 object의 방향을 카메라가 보는 방향으로 설정
                float dz = obj.transform.position.z - ARCamera.transform.position.z;
                float dx = obj.transform.position.x - ARCamera.transform.position.x;

                float rotateDegree = Mathf.Atan2(dz, dx) * Mathf.Rad2Deg;
                obj.transform.rotation = Quaternion.Euler(0f, -rotateDegree, 0f);

                //먼먼이 object 사이즈 변경
                //먼먼이 object 사이즈 변경
                obj.transform.localScale = new Vector3(1, 1, 1);

                anchor = m_NewPlanes[i].CreateAnchor(m_NewPlanes[i].CenterPose);
                obj.transform.parent = anchor.transform;

                munmuns.Add(obj);
                planes.Add(planeObject);
                munmuns_activationFlag.Add(true);

                //Debug.Log("position: " + anchor.transform.localPosition);
                break;
            }

        }
    }
}