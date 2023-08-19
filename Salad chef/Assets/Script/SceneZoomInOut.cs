using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneZoomInOut : MonoBehaviour
{
    [SerializeField]
    private Transform t1;
    [SerializeField]
    private Transform t2;
    [SerializeField]
    private Camera cam;

    private void Update()
    {
        FixedCameraFollowSmooth(cam, t1, t2);
    }
    public void FixedCameraFollowSmooth(Camera cam, Transform t1, Transform t2)
    {
        // How many units should we keep from the players
        float zoomFactor = 1.5f;
        float followTimeDelta = 0.8f;

        // Midpoint we're after
        Vector3 midpoint = (t1.position + t2.position) / 2f;

        // Distance between objects
        float distance = (t1.position - t2.position).magnitude;
        if (distance < 4)
        {
            distance = 4f;
        }

        // Move camera a certain distance
        Vector3 cameraDestination = midpoint - cam.transform.forward * distance * zoomFactor;

        // Adjust ortho size if we're using one of those
        if (cam.orthographic)
        {
            cam.orthographicSize = distance;
        }
        cam.transform.position = Vector3.Slerp(cam.transform.position, cameraDestination, followTimeDelta);

        if ((cameraDestination - cam.transform.position).magnitude <= 0.05f)
            cam.transform.position = cameraDestination;
    }
}
