using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericDialogueAnimator : DialogueAnimator
{
    public Transform speechBubbleOrigin;

    public override void startTalking()
    {
        ;
    }
    public override void stopTalking()
    {
        ;
    }

    public override Vector3 getSpeechOrigin()
    {
        return speechBubbleOrigin.position;
    }
}