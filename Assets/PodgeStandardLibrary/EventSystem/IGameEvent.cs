using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameEvent
{
    /// <summary>
    /// Code to run on every frame while the event is ongoing. Acts as the co-routine
    /// </summary>
    void RunEvent();
    /// <summary>
    /// Used to end game events early
    /// </summary>
    void EndEvent();
    bool EventCompleted();
}

