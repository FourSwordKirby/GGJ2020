using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

public class InstructionLine : ScriptLine
{
    CharacterExpressions desiredExpression;

    public InstructionLine(string instruction)
    {
        if (instruction.StartsWith("[expression]"))
        {
            string expressionString = instruction.Split(' ')[1];
            desiredExpression = (CharacterExpressions)Enum.Parse(typeof(CharacterExpressions), expressionString);
        }
    }

    public static InstructionLine CreateInstructionLine(string instruction)
    {
        InstructionLine line = new InstructionLine(instruction);

        return line;
    }

    //Change this based on the game implementation
    public override void PerformLine()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<CharacterDialogueAnimator>().changeExpression(desiredExpression);
    }

    public override bool IsFinished()
    {
        return true;
    }

    public override DialogueEngine.LineType GetLineType()
    {
        return DialogueEngine.LineType.SpeakingLine;
    }
}