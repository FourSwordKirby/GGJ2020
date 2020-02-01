using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlsNoClip : MonoBehaviour {

    Vector3 relativePos;
    public Transform target;
    public float distance = 3f;
    public float distanceOffset;
    public float zoomSpeed = 2f;
    public float xSpeed = 300f;
    public float ySpeed = 300f;
    public float yMinLimit = 50f;
    public float yMaxLimit = 180f;

    void Update()
    {
        relativePos = transform.position - (target.position);
        RaycastHit hit;
        if (Physics.Raycast(target.position, relativePos, out hit, distance + 0.5f))
        {
            Debug.DrawLine(target.position, hit.point);
            distanceOffset = Mathf.Clamp(distanceOffset, 0, distance);
        }
        else
        {
            distanceOffset = 0;
        }
    }
    //Clamp the distance offset so that it's never a negative number.

    //Distance is the normal distance of the camera from the target point.

    //The 3rd Person camera script:

    void LateUpdate()
    {
        // view zoom
        //distance -= Input.GetAxis("ScrollWheel") * zoomSpeed;
        if (target != null)
        {
            //float x = Input.GetAxis("MouseX") * xSpeed * 0.02f;
            //float y = Input.GetAxis("MouseY") * ySpeed * 0.02f;
            //y = Mathf.Clamp(y, yMinLimit, yMaxLimit);
            //var rotation = Quaternion.Euler(y, x, 0);
            var position = transform.rotation * new Vector3(0.0f, 0.0f, -distance + distanceOffset) + target.position;
            //transform.rotation = rotation;
            transform.position = position;
        }

    }
}
