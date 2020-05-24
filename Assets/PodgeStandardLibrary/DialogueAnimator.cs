using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueAnimator : MonoBehaviour
{
    public abstract void startTalking();
    public abstract void stopTalking();
    public abstract Vector3 getSpeechOrigin();
}