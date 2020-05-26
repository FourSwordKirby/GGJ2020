using UnityEngine;
using System.Collections;


public enum InputDirection
{
    W,
    NW,
    N,
    NE,
    E,
    SE,
    S,
    SW,
    None
};

public class Parameters
{
    public static InputDirection vectorToDirection(Vector2 inputVector)
    {
        if (inputVector == Vector2.zero)
            return InputDirection.None;

        float angle = Mathf.Atan2(inputVector.y, inputVector.x) * Mathf.Rad2Deg;

        if (angle >= -22.5 && angle < 22.5)
        {
            return InputDirection.E;
        }
        else if (angle >= 22.5 && angle < 67.5)
        {
            return InputDirection.NE;
        }
        else if (angle >= 67.5 && angle < 112.5)
        {
            return InputDirection.N;
        }
        else if (angle >= 112.5 && angle < 157.5)
        {
            return InputDirection.NW;
        }
        else if (angle >= 157.5 || angle < -157.5)
        {
            return InputDirection.W;
        }
        else if (angle >= -157.5 && angle < -112.5)
        {
            return InputDirection.SW;
        }
        else if (angle >= -112.5 && angle < -67.5)
        {
            return InputDirection.S;
        }
        else if (angle >= -67.5 && angle < -22.5)
        {
            return InputDirection.SE;
        }
        return InputDirection.None;
    }
}
