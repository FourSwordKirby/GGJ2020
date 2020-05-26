using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SpeakingLine : ScriptLine
{
    SpeakingLineContent content;
    DialogueAnimator speakerAnimator;

    public SpeakingLine(string speaker, string lineText, int lineNumber)
    {
        content = new SpeakingLineContent(speaker, lineText, lineNumber);

        speakerAnimator = GameObject.Find(speaker).GetComponent<DialogueAnimator>();
    }

    public static SpeakingLine CreateSpeakingLine(string speaker, string lineText, int lineNumber)
    {
        SpeakingLine line = new SpeakingLine(speaker, lineText, lineNumber);

        return line;
    }

    //Change this based on the game implementation
    public override void PerformLine()
    {
        Vector3 speakerPosition = speakerAnimator.getSpeechOrigin();

        DialogueBubbleUI.instance.DisplaySpeechBubble(content, speakerPosition);
    }

    public override bool IsFinished()
    {
        return DialogueBubbleUI.instance.ready;
    }

    public override DialogueEngine.LineType GetLineType()
    {
        return DialogueEngine.LineType.SpeakingLine;
    }
}