using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class TriggerGoal : IGoal
{
    public ITrigger trigger;

    public bool GoalCompleted()
    {
        return trigger.TriggerCompleted();
    }
}