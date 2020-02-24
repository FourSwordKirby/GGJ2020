using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SpeakingLine: ScriptLine
{
    string speaker;
    string spokenLine;

    DialogueAnimator speakerAnimator;

    public SpeakingLine(string speaker, string spokenLine)
    {
        this.speaker = speaker;
        this.spokenLine = spokenLine;

        speakerAnimator = GameObject.Find(speaker).GetComponent<DialogueAnimator>();
    }

    public override void PerformLine()
    {
        Vector3 speakerPosition = speakerAnimator.getSpeechOrigin();

        DialogueUIController.instance.displaySpeechBubble(spokenLine, speakerPosition);
    }

    internal override bool IsFinished()
    {
        return DialogueUIController.instance.ready;
    }
}