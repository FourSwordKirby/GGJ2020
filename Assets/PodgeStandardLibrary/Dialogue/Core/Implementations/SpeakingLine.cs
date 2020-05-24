using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public struct SpeakingLineContent
{
    public string speaker;
    public string lineText;
    public int lineNumber;

    public SpeakingLineContent(string speaker, string lineText, int lineNumber)
    {
        this.speaker = speaker;
        this.lineText = lineText;
        this.lineNumber = lineNumber;
    }
}

public class SpeakingLine: ScriptLine
{
    SpeakingLineContent content = new SpeakingLineContent();
    DialogueAnimator speakerAnimator;

    public SpeakingLine(string speaker, string lineText, int lineNumber)
    {
        content = new SpeakingLineContent(speaker, lineText, lineNumber);

        speakerAnimator = GameObject.Find(speaker).GetComponent<DialogueAnimator>();
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