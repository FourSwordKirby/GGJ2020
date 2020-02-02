using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeechAsset : MonoBehaviour
{
    public Animator animator;
    public TextMeshPro textMesh;
    public Transform anchorPoint;

    public float displayModifier;

    private void Start()
    {
        animator.speed = displayModifier == 0 ? 1 : displayModifier;
    }

    public void deployAt(Vector3 speakerPosition)
    {
        animator.SetBool("Deployed", true);
        this.transform.position = speakerPosition - anchorPoint.localPosition;
    }

    public void hide()
    {
        animator.SetBool("Deployed", false);
    }

    public void setText(string text)
    {
        textMesh.text = text;
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
