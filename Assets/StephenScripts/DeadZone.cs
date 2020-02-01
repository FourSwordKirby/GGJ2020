using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range
{
    public float Min;
    public float Max;
}

[Serializable]
public class DeadZone
{
    public Range X;
    public Range Y;
    public Range Z;
}
