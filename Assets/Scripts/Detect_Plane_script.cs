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

    /// <summary>
    /// The Unity Update method.
    /// </summary>
    public void Update()
    {
        // Check that motion tracking is tracking.
        if (Session.Status != SessionStatus.Tracking)
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
                GameObject obj = Instantiate(mun, new Vector3(m_NewPlanes[i].CenterPose.position.x, m_NewPlanes[i].CenterPose.position.y, m_NewPlanes[i].CenterPose.position.z), Quaternion.identity, transform);
                obj.transform.localScale = new Vector3(1, 1, 1);
                Anchor anchor = m_NewPlanes[i].CreateAnchor(m_NewPlanes[i].CenterPose);
                obj.transform.parent = anchor.transform;
                break;
            }

        }

        /*
        GameObject obj = Instantiate(mun, Vector3.zero, Quaternion.identity);
        obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        obj.GetComponent<DetectedPlaneVisualizer>().Initialize(m_NewPlanes[0]);
        */
    }
}
