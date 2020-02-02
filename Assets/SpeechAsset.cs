using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeechAsset : MonoBehaviour
{
    public bool loggable;

    public MeshRenderer textFrame;
    public TextMeshPro textMesh;
    public Animator animator;

    public Transform anchorPoint;
    public float displayModifier;
    public Color focusColor;
    public Color blurColor;

    private void Start()
    {
        animator.speed = displayModifier == 0 ? 1 : displayModifier;
    }

    //currently the bubble only displays facing left or right depending on the argument
    //in the future we want it to display based on any number of relative positions.
    public void deployAt(Vector3 speakerPosition, bool isLeftSide)
    {
        animator.SetBool("Deployed", true);
        if(isLeftSide)
            this.transform.position = speakerPosition - anchorPoint.localPosition;
        else
        {
            textFrame.transform.localScale -= Vector3.right * textFrame.transform.localScale.x * 2;
            this.transform.position = speakerPosition - anchorPoint.localPosition + Vector3.right * anchorPoint.localPosition.x * 2;
        }
    }

    public void focus()
    {
        textFrame.material.color = focusColor;
    }

    public void blur()
    {
        textFrame.material.color = blurColor;
    }

    public void hide()
    {
        animator.SetBool("Deployed", false);
    }

    public void setText(string text)
    {
        textMesh.text = text;
    }

    public void cleanup()
    {
        if (!loggable)
            destroy();
    }

    public void destroy()
    {
        Destroy(this.gameObject);
    }
}
