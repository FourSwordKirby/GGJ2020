using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEncounter
{
    void InitEncounter();
    void CleanupEncounter();
    bool EncounterCompleted();
}
