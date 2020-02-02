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

    public void deployAt(Vector3 speakerPosition)
    {
        animator.SetBool("Deployed", true);
        this.transform.position = speakerPosition - anchorPoint.localPosition;
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
