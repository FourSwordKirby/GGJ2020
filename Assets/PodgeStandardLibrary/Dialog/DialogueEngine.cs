using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DialogueEngine
{
    public static List<ScriptLine> CreateDialogueComponents(string text)
    {
        List<string> rawLines = new List<string>(text.Split('\n'));
        rawLines = rawLines.Select(x => x.Trim()).ToList();
        rawLines = rawLines.Where(x => x != "").ToList();

        List<ScriptLine> processedLines = new List<ScriptLine>();

        string currentSpeaker = "";
        int speakingLineNumber = 0;
        for(int i = 0; i < rawLines.Count; i++)
        {
            ScriptLine processedLine = null;

            string line = rawLines[i];
            switch (GetLineType(line))
            {
                case LineType.SpeakingLine:
                    string speaker = GetSpeaker(line);
                    if (speaker != "")
                        currentSpeaker = speaker;
                    else if (currentSpeaker == "")
                        Debug.LogWarning("Speaker not specified");
                    processedLine = new SpeakingLine(currentSpeaker, GetSpokenLine(line), speakingLineNumber);
                    speakingLineNumber++;
                    break;
                case LineType.Instruction:
                    processedLine = new InstructionLine(line);
                    break;
            }
            processedLines.Add(processedLine);
        }

        return processedLines;
    }

    static LineType GetLineType(string line)
    {
        if (line.StartsWith("[expression]"))
            return LineType.Instruction;
        else
           return LineType.SpeakingLine;
    }

    public static string GetSpeaker(string line)
    {
        string[] dialoguePieces = line.Split(':');

        if (dialoguePieces.Length > 1)
            return dialoguePieces[0];
        else
            return "";
    }

    internal static string GetSpokenLine(string line)
    {
        string[] dialoguePieces = line.Split(':');

        if (dialoguePieces.Length > 1)
            return dialoguePieces[1];
        else
            return line;
    }

    public enum LineType
    {
        SpeakingLine,
        Instruction
    }
}
