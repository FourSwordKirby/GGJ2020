using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterExpressionAnimator : MonoBehaviour
{
    public Animator animator;

    public MeshRenderer expressionRendererFront;
    public SpriteRenderer expressionRendererBack;

    public List<Texture2D> expressions;
    public List<Sprite> expressionSprites;

    public Expressions? targetExpression = null;

    public void changeExpression(Expressions expression)
    {
        targetExpression = expression;
        animator.SetTrigger("NormalExpression");
    }

    private void animateExpression()
    {
        if (targetExpression.HasValue)
        {
            expressionRendererFront.material.SetTexture("_MainTex", expressions[(int)targetExpression.Value]);
            expressionRendererBack.sprite = expressionSprites[(int)targetExpression.Value];
        }
        else
        {
            Debug.LogWarning("animateExpression called, but no targetExpression was set.");
            expressionRendererFront.material.SetTexture("_MainTex", expressions[0]);
            expressionRendererBack.sprite = expressionSprites[0];
        }
    }

    public enum Expressions
    {
        normal = 0,
        smile = 1,
        yandere = 2,
        think = 3
    }
}