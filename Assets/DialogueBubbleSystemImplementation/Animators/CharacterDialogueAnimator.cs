using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDialogueAnimator : DialogueAnimator
{
    public Animator animator;
    public CharacterExpressionAnimator expressionAnimator;

    public Transform speechBubbleOrigin;

    public override void startTalking()
    {
        animator.SetBool("Talking", true);
    }

    public override void stopTalking()
    {
        animator.SetBool("Talking", false);
    }

    public void changeExpression(CharacterExpressions expression)
    {
        expressionAnimator.changeExpression(expression);
    }

    public override Vector3 getSpeechOrigin()
    {
        return speechBubbleOrigin.position;
    }
}