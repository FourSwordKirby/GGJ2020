using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IDialogueBubble
{
    void SetDialogueBubbleContent(SpeakingLineContent content);
    void DeployAt(Vector3 speakerPosition, Vector2 displacementVector);

    void Show();
    void Hide();

    void Focus();
    void Blur();

    void Cleanup();

    void Destroy();
}
