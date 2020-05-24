using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent : MonoBehaviour
{
    public abstract void RunEvent();
    /// <summary>
    /// Used to end game events early
    /// </summary>
    public abstract void EndEvent();
    public abstract bool EventCompleted();
}

