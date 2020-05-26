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

    public static InputDirection getInputDirection()
    {
        return Parameters.vectorToDirection(getDirection());
    }

    //We need to refactor everything orz
    public static InputDirection getInputDirection(PlayerDesignations player)
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

    public static bool DirectionDown(InputDirection dir)
    {
        if (!gameplayEnabled)
            return false;

        //Hacky, probably should fix to be correct later
        InputDirection currentInput = getInputDirection();
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

    public static bool confirmInputDown()
    {
        if (!gameplayEnabled)
            return false;

        return Input.GetButtonDown("Confirm");
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

    static InputDirection currentHeldDir = InputDirection.None;

    public static InputDirection getInputDirectionDown()
    {
        if (!gameplayEnabled)
            return InputDirection.None;

        InputDirection currentDir = getInputDirection();
        //Hacky, probably should fix to be correct later
        if (currentHeldDir == currentDir)
            return InputDirection.None;

        currentHeldDir = currentDir;
        return currentHeldDir;
    }

    public static bool JumpInputDown(PlayerDesignations player)
    {
        return Input.GetButtonDown("P1 Jump");
    }

    public static bool ActionInputDown(PlayerDesignations player)
    {
        return Input.GetButtonDown("Action");
    }


    public static bool ActionInputHeld(PlayerDesignations player)
    {
        return Input.GetButton("Action");
    }

    internal static bool AssistOneInputDown()
    {
        if (!gameplayEnabled)
            return false;

        return Input.GetButtonDown("AssistOne");
    }

    internal static bool AssistOneInputHeld()
    {
        if (!gameplayEnabled)
            return false;

        return Input.GetButton("AssistOne");
    }

    internal static bool AssistTwoInputDown()
    {
        if (!gameplayEnabled)
            return false;

        return Input.GetButtonDown("AssistTwo");
    }

    internal static bool AssistTwoInputHeld()
    {
        if (!gameplayEnabled)
            return false;

        return Input.GetButton("AssistTwo");
    }

    internal static bool AssistThreeInputDown()
    {
        if (!gameplayEnabled)
            return false;

        return Input.GetButtonDown("AssistThree");
    }

    internal static bool AssistThreeInputHeld()
    {
        if (!gameplayEnabled)
            return false;

        return Input.GetButton("AssistThree");
    }

    //******************Deprecated research project fighting game inputs*******************
    //*************************************************************************************
    public static bool jumpInputDown(PlayerDesignations player)
    {
        if (player == PlayerDesignations.Player1)
            return Input.GetButtonDown("P1 Jump");
        else if (player == PlayerDesignations.Player2)
            return Input.GetButtonDown("P2 Jump");
        else
            return false;
    }

    public static bool attackInputDown(PlayerDesignations player)
    {
        if (player == PlayerDesignations.Player1)
            return Input.GetButtonDown("P1 Attack");
        else if (player == PlayerDesignations.Player2)
            return Input.GetButtonDown("P2 Attack");
        else
            return false;
    }

    public static bool specialInputDown(PlayerDesignations player)
    {
        if (player == PlayerDesignations.Player1)
            return Input.GetButtonDown("P1 Special");
        else if (player == PlayerDesignations.Player2)
            return Input.GetButtonDown("P2 Special");
        else
            return false;
    }

    public static bool shieldInputDown(PlayerDesignations player)
    {
        if (player == PlayerDesignations.Player1)
            return Input.GetButtonDown("P1 Shield");
        else if (player == PlayerDesignations.Player2)
            return Input.GetButtonDown("P2 Shield");
        else
            return false;
    }

    public static bool dashInputDown(PlayerDesignations player)
    {
        if (player == PlayerDesignations.Player1)
            return Input.GetButtonDown("P1 Dash");
        else if (player == PlayerDesignations.Player2)
            return Input.GetButtonDown("P2 Dash");
        else
            return false;
    }

    public static bool superInputDown(PlayerDesignations player)
    {
        if (player == PlayerDesignations.Player1)
            return Input.GetButtonDown("P1 Super");
        else if (player == PlayerDesignations.Player2)
            return Input.GetButtonDown("P2 Super");
        else
            return false;
    }

    public static bool pauseInputDown(PlayerDesignations player)
    {
        return Input.GetButtonDown("Pause");
    }


    public static bool jumpInputHeld(PlayerDesignations player)
    {
        if (player == PlayerDesignations.Player1)
            return Input.GetButton("P1 Jump");
        else if (player == PlayerDesignations.Player2)
            return Input.GetButton("P2 Jump");
        else
            return false;
    }

    public static bool attackInputHeld(PlayerDesignations player)
    {
        if (player == PlayerDesignations.Player1)
            return Input.GetButton("P1 Attack");
        else if (player == PlayerDesignations.Player2)
            return Input.GetButton("P2 Attack");
        else
            return false;
    }

    public static bool specialInputHeld(PlayerDesignations player)
    {
        if (player == PlayerDesignations.Player1)
            return Input.GetButton("P1 Special");
        else if (player == PlayerDesignations.Player2)
            return Input.GetButton("P2 Special");
        else
            return false;
    }

    public static bool shieldInputHeld(PlayerDesignations player)
    {
        if (player == PlayerDesignations.Player1)
            return Input.GetButton("P1 Shield");
        else if (player == PlayerDesignations.Player2)
            return Input.GetButton("P2 Shield");
        else
            return false;
    }

    public static bool dashInputHeld(PlayerDesignations player)
    {
        if (player == PlayerDesignations.Player1)
            return Input.GetButton("P1 Dash");
        else if (player == PlayerDesignations.Player2)
            return Input.GetButton("P2 Dash");
        else
            return false;
    }

    public static bool superInputHeld(PlayerDesignations player)
    {
        if (player == PlayerDesignations.Player1)
            return Input.GetButton("P1 Super");
        else if (player == PlayerDesignations.Player2)
            return Input.GetButton("P2 Super");
        else
            return false;
    }

    public static bool pauseInputHeld(PlayerDesignations player)
    {
        return Input.GetButton("Pause");
    }

    //Fighting game inputs
    /*
    public static bool jumpInputDown(PlayerDesignations player)
    {
        if (player == PlayerDesignations.Player1)
            return Input.GetButtonDown("P1 Jump");
        else if (player = GameManager.instance.p2)
            return Input.GetButtonDown("P2 Jump");
        else
            return false;
    }

    public static bool attackInputDown(PlayerDesignations player)
    {
        if (player == PlayerDesignations.Player1)
            return Input.GetButtonDown("P1 Attack");
        else if (player == PlayerDesignations.Player2)
            return Input.GetButtonDown("P2 Attack");
        else
            return false;
    }

    public static bool specialInputDown(PlayerDesignations player)
    {
        if (player == PlayerDesignations.Player1)
            return Input.GetButtonDown("P1 Special");
        else if (player == PlayerDesignations.Player2)
            return Input.GetButtonDown("P2 Special");
        else
            return false;
    }

    public static bool shieldInputDown(PlayerDesignations player)
    {
        if (player == PlayerDesignations.Player1)
            return Input.GetButtonDown("P1 Shield");
        else if (player == PlayerDesignations.Player2)
            return Input.GetButtonDown("P2 Shield");
        else
            return false;
    }

    public static bool dashInputDown(PlayerDesignations player)
    {
        if (player == PlayerDesignations.Player1)
            return Input.GetButtonDown("P1 Dash");
        else if (player == PlayerDesignations.Player2)
            return Input.GetButtonDown("P2 Dash");
        else
            return false;
    }

    public static bool superInputDown(PlayerDesignations player)
    {
        if (player == PlayerDesignations.Player1)
            return Input.GetButtonDown("P1 Super");
        else if (player == PlayerDesignations.Player2)
            return Input.GetButtonDown("P2 Super");
        else
            return false;
    }

    public static bool pauseInputDown(PlayerDesignations player)
    {
        return Input.GetButtonDown("Pause");
    }


    public static bool jumpInputHeld(PlayerDesignations player)
    {
        if (player == PlayerDesignations.Player1)
            return Input.GetButton("P1 Jump");
        else if (player == PlayerDesignations.Player2)
            return Input.GetButton("P2 Jump");
        else
            return false;
    }

    public static bool attackInputHeld(PlayerDesignations player)
    {
        if (player == PlayerDesignations.Player1)
            return Input.GetButton("P1 Attack");
        else if (player == PlayerDesignations.Player2)
            return Input.GetButton("P2 Attack");
        else
            return false;
    }

    public static bool specialInputHeld(PlayerDesignations player)
    {
        if (player == PlayerDesignations.Player1)
            return Input.GetButton("P1 Special");
        else if (player == PlayerDesignations.Player2)
            return Input.GetButton("P2 Special");
        else
            return false;
    }

    public static bool shieldInputHeld(PlayerDesignations player)
    {
        if (player == PlayerDesignations.Player1)
            return Input.GetButton("P1 Shield");
        else if (player == PlayerDesignations.Player2)
            return Input.GetButton("P2 Shield");
        else
            return false;
    }

    public static bool dashInputHeld(PlayerDesignations player)
    {
        if (player == PlayerDesignations.Player1)
            return Input.GetButton("P1 Dash");
        else if (player == PlayerDesignations.Player2)
            return Input.GetButton("P2 Dash");
        else
            return false;
    }

    public static bool superInputHeld(PlayerDesignations player)
    {
        if (player == PlayerDesignations.Player1)
            return Input.GetButton("P1 Super");
        else if (player == PlayerDesignations.Player2)
            return Input.GetButton("P2 Super");
        else
            return false;
    }

    public static bool pauseInputHeld(PlayerDesignations player)
    {
        return Input.GetButton("Pause");
    }
    */
}

public enum PlayerDesignations
{
    Player1,
    Player2
}
