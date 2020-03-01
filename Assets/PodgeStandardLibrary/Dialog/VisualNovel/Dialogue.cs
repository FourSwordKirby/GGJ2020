using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class Dialogue
{
    List<ScriptLine> lines;
    public int readCount;
    public int currentPosition;
    public bool IsFinished { get => readCount == lines.Count; }

    //temporary variables for getting this working with the current implementation
    public int speakingLineCount {
        get => lines.Where(x => x.GetLineType() == DialogueEngine.LineType.SpeakingLine).ToList().Count;
    }

    public Dialogue (string fullDialogue)
    {
        lines = DialogueEngine.CreateDialogueComponents(fullDialogue);
        readCount = 0;
        currentPosition = 0;
    }

    public void Init()
    {
        readCount = 0;
        currentPosition = 0;
    }

    public ScriptLine GetNextLine()
    {
        ScriptLine line = lines[currentPosition];
        readCount++;
        currentPosition++;

        return line;
    }
}