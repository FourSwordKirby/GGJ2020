using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public abstract class ScriptLine
{
    public abstract void PerformLine();
    internal abstract bool IsFinished();
}