using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class EventGoal: IGoal
{
    public IGameEvent linkedGameEvent;

    public bool GoalCompleted()
    {
        return linkedGameEvent.EventCompleted();
    }
}