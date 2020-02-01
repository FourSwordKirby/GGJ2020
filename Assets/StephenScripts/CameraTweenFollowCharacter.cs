using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTweenFollowCharacter : MonoBehaviour
{
    public Transform TransformToFollow;

    public Vector3 OffsetVectorToTransformToFollow;
    public Quaternion OriginalAngle;

    public Vector3 TargetLocation;
    public Quaternion TargetRotation;

    public float TranslatationLerpFactor = .5f;
    public float RotationLerpFactor = .5f;

    // Start is called before the first frame update
    void Start()
    {
        OriginalAngle = this.transform.rotation;

        if (TransformToFollow != null)
        {
            OffsetVectorToTransformToFollow = TransformToFollow.position - this.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (TransformToFollow != null)
        {
            TargetLocation = TransformToFollow.position - OffsetVectorToTransformToFollow;
        }

        this.transform.position = Vector3.Lerp(this.transform.position, TargetLocation, TranslatationLerpFactor);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, TargetRotation, RotationLerpFactor);
    }
}
