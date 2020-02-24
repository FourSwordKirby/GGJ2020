using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class Dialogue
{
    List<ScriptLine> lines;
    List<ScriptLine> unreadLines;
    public int currentPosition;
    public bool IsFinished { get => unreadLines.Count == 0; }

    //temporary variables for getting this working with the current implementation
    public int dialogueLineCount { get => lines.Count; }

    public Dialogue (string fullDialogue)
    {
        lines = DialogueEngine.CreateDialogueComponents(fullDialogue);
        unreadLines = lines;
        currentPosition = 0;
    }

    public void Init()
    {
        unreadLines = lines;
        currentPosition = 0;
    }

    public ScriptLine GetNextLine()
    {
        ScriptLine line = lines[currentPosition];
        unreadLines.Remove(line);
        currentPosition++;

        return line;
    }
}