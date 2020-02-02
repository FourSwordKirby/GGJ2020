using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterExperssion : MonoBehaviour
{
    public Animator animator;

    public MeshRenderer expressionRendererFront;
    public SpriteRenderer expressionRendererBack;

    public List<Texture2D> expressions;
    public List<Sprite> expressionSprites;

    public void changeExpression(Expressions expression)
    {
        print("called");
        switch (expression)
        {
            case Expressions.normal:
                animator.SetTrigger("NormalExpression");
                break;
            case Expressions.smile:
                animator.SetTrigger("UpsetExpression");
                break;
            case Expressions.yandere:
                animator.SetTrigger("YandereExpression");
                break;
        }
    }

    private void animateExpression(Expressions expression)
    {
        switch (expression)
        {
            case Expressions.normal:
                expressionRendererFront.material.SetTexture("_MainTex", expressions[0]);
                expressionRendererBack.sprite = expressionSprites[0];
                break;
            case Expressions.smile:
                expressionRendererFront.material.SetTexture("_MainTex", expressions[1]);
                expressionRendererBack.sprite = expressionSprites[1];
                break;
            case Expressions.yandere:
                expressionRendererFront.material.SetTexture("_MainTex", expressions[2]);
                expressionRendererBack.sprite = expressionSprites[2];
                break;
        }
    }

    public enum Expressions
    {
        normal,
        smile,
        yandere
    }
}