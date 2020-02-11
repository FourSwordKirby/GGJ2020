using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraMan : MonoBehaviour
{
    public enum ZTrackingStrategy
    {
        Exact,
        Frozen,
        Lerp
    }

    public Camera MyCamera;
    public Camera ProjectedCamera;

    public bool EnableInEditMode = false;
    public bool EnableTransformTracking = true;
    public bool IsCinematic = false;
    public Transform TransformToTrack;

    public Transform DefaultTransform;

    public ZTrackingStrategy ZTracking = ZTrackingStrategy.Exact;
    public Transform LowZTransform;
    public Transform HighZTransform;
    public Bounds DeadZone = new Bounds();

    public Vector3 OffsetVectorToTrackedTransform;

    public Vector3 TargetPosition;
    public Quaternion TargetRotation;

    public float TranslatationLerpFactor = .5f;
    public float RotationLerpFactor = .5f;

    // Start is called before the first frame update
    void Start()
    {
        //if (TransformToTrack != null)
        //{
        //    OffsetVectorToTrackedTransform = TransformToTrack.position - this.transform.position;
        //}
    }

    public void StartCinematicMode(Transform cameraPosition)
    {
        IsCinematic = true;
        TargetPosition = cameraPosition.position;
        TargetRotation = cameraPosition.rotation;

        //make sure the projected Camera is always in the final desired position for accurate worldToScreen tracking
        ProjectedCamera.transform.position = TargetPosition;
        ProjectedCamera.transform.rotation = TargetRotation;
    }

    public void EndCinematicMode()
    {
        IsCinematic = false;
    }

    // Update is called once per frame
    void Update()
    {
        //make sure the projected Camera is always in the final desired position for accurate worldToScreen tracking
        ProjectedCamera.transform.position = TargetPosition;
        ProjectedCamera.transform.rotation = TargetRotation;

        // Allow camera logic in Editor if EnableInEditMode is true
        if (!Application.isPlaying && !EnableInEditMode)
        {
            return;
        }

        if (IsCinematic)
        {
            // This effectively means the CameraMan gives up the Camera.
            MyCamera.transform.position = Vector3.Lerp(MyCamera.transform.position, TargetPosition, TranslatationLerpFactor);
            MyCamera.transform.rotation = Quaternion.Lerp(MyCamera.transform.rotation, TargetRotation, RotationLerpFactor);
        }
        else if (EnableTransformTracking)
        {
            if (TransformToTrack == null)
            {
                Debug.LogWarning("CameraMan has transform tracking enabled, but no transform was provided.");
                return;
            }

            DeadZone.center = OffsetVectorToTrackedTransform + this.transform.position;
            if (!DeadZone.Contains(TransformToTrack.position))
            {
                Vector3 closestPoint = DeadZone.ClosestPoint(TransformToTrack.position);
                //Debug.Log($"Closest Point: {closestPoint}");
                Vector3 moveAmount = TransformToTrack.position - closestPoint;
                //Debug.Log($"Camera moveAmount: {moveAmount}");
                TargetPosition = this.transform.position + moveAmount;
            }
            else
            {
                TargetPosition = this.transform.position;
            }

            TargetRotation = this.transform.rotation;
            
            if (ZTracking == ZTrackingStrategy.Frozen)
            {
                // CameraMan stays locked to his the XY-plane
                TargetPosition.z = this.transform.position.z;
            }
            else if (ZTracking == ZTrackingStrategy.Lerp)
            {
                // CameraMan stays locked to his the XY-plane
                TargetPosition.z = this.transform.position.z;

                float minZ = DeadZone.min.z;
                float maxZ = DeadZone.max.z;
                float zPoint = (TransformToTrack.position.z - minZ) / (maxZ - minZ);
                Vector3 targetCameraPosition = Vector3.Lerp(LowZTransform.position, HighZTransform.position, zPoint);
                Quaternion targetCameraRotation = Quaternion.Lerp(LowZTransform.rotation, HighZTransform.rotation, zPoint);

                MyCamera.transform.position = Vector3.Lerp(MyCamera.transform.position, targetCameraPosition, TranslatationLerpFactor);
                MyCamera.transform.rotation = Quaternion.Lerp(MyCamera.transform.rotation, targetCameraRotation, RotationLerpFactor);
            }
            
            this.transform.position = TargetPosition;
            this.transform.rotation = TargetRotation;
        }
        else
        {
            // Do nothing if no other mode is enabled.
        }
    }
    
    //Used to wait for the camera to get into position so that we can determine where to put the speech bubble
    //This approach is flawed as we should ideally know where the speech bubbles should go given the position of the camera and the actors. 
    //In the future we should be able to do this check without having to wait for the camera to get into position
    public bool InDesiredPosition()
    {
        return (MyCamera.transform.position - TargetPosition).magnitude < 0.1f;
    }

    public void MoveCameraToDefault()
    {
        MyCamera.transform.CopyValues(DefaultTransform);
    }

    public void MoveCameraToHighZ()
    {
        MyCamera.transform.CopyValues(HighZTransform);
    }

    public void MoveCameraToLowZ()
    {
        MyCamera.transform.CopyValues(LowZTransform);
    }

    public void SaveCameraValuesToDefault()
    {
        DefaultTransform.CopyValues(MyCamera.transform);
    }

    public void SaveCameraValuesToHighZ()
    {
        HighZTransform.CopyValues(MyCamera.transform);
    }

    public void SaveCameraValuesToLowZ()
    {
        LowZTransform.CopyValues(MyCamera.transform);
    }

    public void SaveCameraValuesToNewTransform()
    {
        GameObject obj = new GameObject("Saved Camera Position");
        obj.transform.SetParent(this.transform);
        obj.transform.CopyValues(MyCamera.transform);
    }
}
