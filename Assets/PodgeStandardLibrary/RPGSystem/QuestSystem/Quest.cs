using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class Quest
{
    string QuestName;
    string QuestSummary;

    public List<Quest> SubQuests;
    public List<IGoal> RequiredGoals;

    public bool QuestCompleted()
    {
        return SubQuests.TrueForAll(x => x.QuestCompleted()) && RequiredGoals.TrueForAll(x => x.GoalCompleted());
    }
}