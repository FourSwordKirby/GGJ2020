using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public abstract class ScriptLine
{
    public abstract DialogueEngine.LineType GetLineType();

    public abstract void PerformLine();
    public abstract bool IsFinished();
}