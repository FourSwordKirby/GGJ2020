using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

public class InstructionLine : ScriptLine
{
    CharacterExpressionAnimator.Expressions desiredExpression;

    public InstructionLine(string instruction)
    {
        if (instruction.StartsWith("[expression]"))
        {
            string expressionString = instruction.Split(' ')[1];
            desiredExpression = (CharacterExpressionAnimator.Expressions)Enum.Parse(typeof(CharacterExpressionAnimator.Expressions), expressionString);
        }
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