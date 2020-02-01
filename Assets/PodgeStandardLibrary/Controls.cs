using UnityEngine;
using System.Collections;
using System;

public class Controls {

    private static bool gameplayEnabled = true;

    public static void DisableGameplayControls()
    {
        gameplayEnabled = false;
    }

    public static void EnableGameplayControls()
    {
        gameplayEnabled = true;
    }

    public static Vector2 getDirection(bool relativeToCamera = false)
    {
        if (!gameplayEnabled)
            return Vector2.zero;

        float xAxis = 0;
        float yAxis = 0;

        if (Mathf.Abs(Input.GetAxis("P1 Horizontal")) > Mathf.Abs(Input.GetAxis("P1 Keyboard Horizontal")))
            xAxis = Input.GetAxis("P1 Horizontal");
        else
            xAxis = Input.GetAxis("P1 Keyboard Horizontal");
        if (Mathf.Abs(Input.GetAxis("P1 Vertical")) > Mathf.Abs(Input.GetAxis("P1 Keyboard Vertical")))
            yAxis = Input.GetAxis("P1 Vertical");
        else
            yAxis = Input.GetAxis("P1 Keyboard Vertical");

        if(relativeToCamera)
        {

            //float perspectiveRotation = -Mathf.Deg2Rad * Camera.current.gameObject.transform.rotation.y;

            //Explanation here: https://matthew-brett.github.io/teaching/rotation_2d.html
            //I can't remember calc lol
            //float true_xAxis = xAxis * Mathf.Cos(perspectiveRotation) - yAxis * Mathf.Sin(perspectiveRotation);
            //float true_yAxis = (xAxis * Mathf.Sin(perspectiveRotation) + yAxis * Mathf.Cos(perspectiveRotation));
            //return new Vector2(true_xAxis, true_yAxis);

            throw new Exception("Please imeplement properly for the associated game");
        }
        return new Vector2(xAxis, yAxis);
    }

    public static Parameters.InputDirection getInputDirection()
    {
        return Parameters.vectorToDirection(getDirection());
    }

    //We need to refactor everything orz
    public static Parameters.InputDirection getInputDirection(int player)
    {
        return Parameters.vectorToDirection(getDirection());
    }

    public static Vector2 getAimDirection(bool relativeToCamera = false)
    {
        if (!gameplayEnabled)
            return Vector2.zero;

        float xAxis = 0;
        float yAxis = 0;

        if (Mathf.Abs(Input.GetAxis("AimX")) > Mathf.Abs(Input.GetAxis("Keyboard AimX")))
            xAxis = Input.GetAxis("AimX");
        else
            xAxis = Input.GetAxis("Keyboard AimX");

        if (Mathf.Abs(Input.GetAxis("AimY")) > Mathf.Abs(Input.GetAxis("Keyboard AimY")))
            yAxis = Input.GetAxis("AimY");
        else
            yAxis = Input.GetAxis("Keyboard AimY");

        if (relativeToCamera)
        {

            //float perspectiveRotation = -Mathf.Deg2Rad * Camera.current.gameObject.transform.rotation.y;

            //Explanation here: https://matthew-brett.github.io/teaching/rotation_2d.html
            //I can't remember calc lol
            //float true_xAxis = xAxis * Mathf.Cos(perspectiveRotation) - yAxis * Mathf.Sin(perspectiveRotation);
            //float true_yAxis = (xAxis * Mathf.Sin(perspectiveRotation) + yAxis * Mathf.Cos(perspectiveRotation));
            //return new Vector2(true_xAxis, true_yAxis);

            throw new Exception("Please imeplement properly for the associated game");
        }

        return new Vector2(xAxis, yAxis);
    }

    public static bool DirectionDown(Parameters.InputDirection dir)
    {
        if (!gameplayEnabled)
            return false;

        //Hacky, probably should fix to be correct later
        Parameters.InputDirection currentInput = getInputDirection();
        return currentInput == dir;
    }

    internal static bool assistInputDown()
    {
        if (!gameplayEnabled)
            return false;

        return Input.GetButtonDown("P1 Special");
    }

    public static bool dialogAdvanceDown()
    {
        return Input.GetButtonDown("Confirm");
    }

    public static bool cancelInputDown()
    {
        return Input.GetButtonDown("Cancel");
    }

    public static bool confirmInputHeld()
    {
        if (!gameplayEnabled)
            return false;

        return Input.GetButton("Confirm");
    }

    public static bool cancelInputHeld()
    {
        if (!gameplayEnabled)
            return false;

        return Input.GetButton("Cancel");
    }

    public static bool pauseInputDown()
    {
        if (!gameplayEnabled)
            return false;

        return Input.GetButtonDown("Pause");
    }

    public static bool pauseInputHeld()
    {
        if (!gameplayEnabled)
            return false;

        return Input.GetButton("Pause");
    }

    public static bool jumpInputDown()
    {
        return Input.GetButtonDown("P1 Jump");
    }

    public static bool jumpInputHeld()
    {
        return Input.GetButton("P1 Jump");
    }

    //Fighting game inputs
    /*
    public static bool jumpInputDown(int player)
    {
        if (player == GameManager.instance.p1)
            return Input.GetButtonDown("P1 Jump");
        else if (player = GameManager.instance.p2)
            return Input.GetButtonDown("P2 Jump");
        else
            return false;
    }

    public static bool attackInputDown(int player)
    {
        if (player == GameManager.instance.p1)
            return Input.GetButtonDown("P1 Attack");
        else if (player == GameManager.instance.p2)
            return Input.GetButtonDown("P2 Attack");
        else
            return false;
    }

    public static bool specialInputDown(int player)
    {
        if (player == GameManager.instance.p1)
            return Input.GetButtonDown("P1 Special");
        else if (player == GameManager.instance.p2)
            return Input.GetButtonDown("P2 Special");
        else
            return false;
    }

    public static bool shieldInputDown(int player)
    {
        if (player == GameManager.instance.p1)
            return Input.GetButtonDown("P1 Shield");
        else if (player == GameManager.instance.p2)
            return Input.GetButtonDown("P2 Shield");
        else
            return false;
    }

    public static bool dashInputDown(int player)
    {
        if (player == GameManager.instance.p1)
            return Input.GetButtonDown("P1 Dash");
        else if (player == GameManager.instance.p2)
            return Input.GetButtonDown("P2 Dash");
        else
            return false;
    }

    public static bool superInputDown(int player)
    {
        if (player == GameManager.instance.p1)
            return Input.GetButtonDown("P1 Super");
        else if (player == GameManager.instance.p2)
            return Input.GetButtonDown("P2 Super");
        else
            return false;
    }

    public static bool pauseInputDown(int player)
    {
        return Input.GetButtonDown("Pause");
    }


    public static bool jumpInputHeld(int player)
    {
        if (player == GameManager.instance.p1)
            return Input.GetButton("P1 Jump");
        else if (player == GameManager.instance.p2)
            return Input.GetButton("P2 Jump");
        else
            return false;
    }

    public static bool attackInputHeld(int player)
    {
        if (player == GameManager.instance.p1)
            return Input.GetButton("P1 Attack");
        else if (player == GameManager.instance.p2)
            return Input.GetButton("P2 Attack");
        else
            return false;
    }

    public static bool specialInputHeld(int player)
    {
        if (player == GameManager.instance.p1)
            return Input.GetButton("P1 Special");
        else if (player == GameManager.instance.p2)
            return Input.GetButton("P2 Special");
        else
            return false;
    }

    public static bool shieldInputHeld(int player)
    {
        if (player == GameManager.instance.p1)
            return Input.GetButton("P1 Shield");
        else if (player == GameManager.instance.p2)
            return Input.GetButton("P2 Shield");
        else
            return false;
    }

    public static bool dashInputHeld(int player)
    {
        if (player == GameManager.instance.p1)
            return Input.GetButton("P1 Dash");
        else if (player == GameManager.instance.p2)
            return Input.GetButton("P2 Dash");
        else
            return false;
    }

    public static bool superInputHeld(int player)
    {
        if (player == GameManager.instance.p1)
            return Input.GetButton("P1 Super");
        else if (player == GameManager.instance.p2)
            return Input.GetButton("P2 Super");
        else
            return false;
    }

    public static bool pauseInputHeld(int player)
    {
        return Input.GetButton("Pause");
    }
    */
}
