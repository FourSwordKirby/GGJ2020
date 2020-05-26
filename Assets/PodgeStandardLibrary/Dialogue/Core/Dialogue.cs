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

    public Dialogue (List<ScriptLine> lines)
    {
        this.lines = lines;
        readCount = 0;
        currentPosition = 0;
    }

    public void Init()
    {
        readCount = 0;
        currentPosition = 0;
    }

    // Throws an exception if we try to get the next line when we already passed the current line count
    public ScriptLine GetNextLine()
    {
        if(currentPosition < lines.Count)
        {
            ScriptLine line = lines[currentPosition];
            readCount++;
            currentPosition++;

            return line;
        }
        else
            throw new System.Exception("Reached the end of the dialogue, there is no next line");
    }

    public ScriptLine GetPreviousLine()
    {
        if (currentPosition >= 0)
        {
            ScriptLine line = lines[currentPosition];
            currentPosition--;

            return line;
        }
        else
            throw new System.Exception("Reached the start of the dialogue, there is no previous line");
    }
}