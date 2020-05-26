using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public abstract class ScriptLine
{
    public abstract DialogueEngine.LineType GetLineType();

    public abstract void PerformLine();
    public abstract bool IsFinished();
}

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