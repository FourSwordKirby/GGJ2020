using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static void CopyValues(this Transform me, Transform desired)
    {
        me.position = desired.position;
        me.rotation = desired.rotation;
    }
}
