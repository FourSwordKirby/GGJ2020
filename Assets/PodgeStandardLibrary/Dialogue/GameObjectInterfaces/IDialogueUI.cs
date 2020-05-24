using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogueUI
{
    IDialogueBubble DisplaySpeechPrompt(Vector3 location);
    void HideSpeechPrompt(IDialogueBubble speechPrompt);

    IDialogueBubble DeploySpeechBubbleAt(IDialogueBubble speechBubble, Vector3 location);
    void DeploySpeechBubble(IDialogueBubble speechBubble);
    IDialogueBubble GenerateSpeechBubble(SpeakingLineContent content);
    void HideSpeechBubble(IDialogueBubble speechBubble);
}
