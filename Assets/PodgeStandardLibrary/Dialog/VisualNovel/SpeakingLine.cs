using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SpeakingLine: ScriptLine
{
    string speaker;
    string spokenLine;
    int lineNumber;

    DialogueAnimator speakerAnimator;

    
    public SpeakingLine(string speaker, string spokenLine, int lineNumber)
    {
        this.speaker = speaker;
        this.spokenLine = spokenLine;
        this.lineNumber = lineNumber;

        speakerAnimator = GameObject.Find(speaker).GetComponent<DialogueAnimator>();
    }


    //Change this based on the game implementation
    public override void PerformLine()
    {
        Vector3 speakerPosition = speakerAnimator.getSpeechOrigin();

        DialogueUIController.instance.DisplaySpeechBubble(spokenLine, speakerPosition, lineNumber);
    }

    public override bool IsFinished()
    {
        return DialogueUIController.instance.ready;
    }

    public override DialogueEngine.LineType GetLineType()
    {
        return DialogueEngine.LineType.SpeakingLine;
    }
}