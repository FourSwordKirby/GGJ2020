using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class ItemGoal : IGoal
{
    public Dictionary<Item, int> RequiredItems;

    public bool GoalCompleted()
    {
        IRpgPlayer testplayer = null;
        Inventory playerInventory = testplayer.GetInventory();

        foreach(Item item in RequiredItems.Keys)
        {
            if (playerInventory.GetItemCount(item) != RequiredItems[item])
                return false;
        }

        return true;
    }
}