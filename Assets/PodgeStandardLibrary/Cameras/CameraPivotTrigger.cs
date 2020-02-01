using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivotTrigger : MonoBehaviour {

    public float targetRotationY;

    void OnTriggerEnter(Collider col)
    {
        //Hurtbox3D colBox = col.GetComponent<Hurtbox3D>();
        //if (colBox != null && colBox.owner.GetComponent<ShmupPlayer>() != null)
            CameraControlsTopDown3D.instance.targetRotation_Y = targetRotationY;
    }
}
