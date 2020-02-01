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

    public bool EnableInEditMode = false;
    public bool EnableTransformTracking = true;
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
        if (TransformToTrack != null)
        {
            OffsetVectorToTrackedTransform = TransformToTrack.position - this.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying && !EnableInEditMode)
        {
            return;
        }

        if (EnableTransformTracking)
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
                Debug.Log($"Closest Point: {closestPoint}");
                Vector3 moveAmount = TransformToTrack.position - closestPoint;
                Debug.Log($"Camera moveAmount: {moveAmount}");
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
                MyCamera.transform.position = Vector3.Lerp(LowZTransform.position, HighZTransform.position, zPoint);
                MyCamera.transform.rotation = Quaternion.Lerp(LowZTransform.rotation, HighZTransform.rotation, zPoint);
            }
            
            this.transform.position = TargetPosition;
            this.transform.rotation = TargetRotation;
        }
        else
        {
            this.transform.position = Vector3.Lerp(this.transform.position, TargetPosition, TranslatationLerpFactor);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, TargetRotation, RotationLerpFactor);
        }
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
}
