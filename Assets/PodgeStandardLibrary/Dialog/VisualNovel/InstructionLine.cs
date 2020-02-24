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

    public override void PerformLine()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<CharacterDialogueAnimator>().changeExpression(desiredExpression);
    }

    internal override bool IsFinished()
    {
        return true;
    }
}