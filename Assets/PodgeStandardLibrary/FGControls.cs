using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public enum FGButton {
    A,
    B,
    C,
    D
}

public enum FGDirection {
    Dir1,
    Dir2,
    Dir3,
    Dir4,
    Dir5,
    Dir6,
    Dir7,
    Dir8,
    Dir9
}

public enum FGInputGrade
{
    Bad,
    Good,
    Excellent
}

public enum FGDirectionInputType
{
    none,
    motion,
    charge,
    smash
}

public enum FGButtonInputType
{
    none,
    press,
    release
}

public class FGControls:MonoBehaviour {

    private List<(HashSet<FGButton> buttons, FGDirection direction)> inputBuffer = new List<(HashSet<FGButton> buttons, FGDirection direction)>();

    public int maxBufferLength = 5000;

    public static FGControls instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }

    void Update()
    {
        HashSet<FGButton> buttonsPressed = GetButtonsPressed();
        FGDirection fgDir = GetFGDirection(Controls.getInputDirection(), true);
        inputBuffer.Add((buttonsPressed, fgDir));

        if(inputBuffer.Count > maxBufferLength * 2)
        {
            inputBuffer.RemoveRange(0, maxBufferLength);
        }

        //print(fgDir);

        //if (Input.GetKeyDown(KeyCode.Space))
        //    print(GradeInput(FGDirectionInputType.charge, FGButtonInputType.none, new List<FGDirection> { FGDirection.Dir4, FGDirection.Dir6 }));


        //Axes and keyboard controls can get kind of bad about getting clean inputs. Do some testing later.
        if (Detect236Input())
            print("236");
        if (Detect214Input())
            print("214");
        if (Detect623Input())
            print("623");
        if (Detect22Input())
            print("22");
        //if (Detect28ChargeInput())
        //    print("28");
    }

    public FGInputGrade GradeInput(FGDirectionInputType dirType, FGButtonInputType buttonType, 
                                    List<FGDirection>InputDir = null, List<FGButton>InputButtons = null)
    {
        float dirGrade = 1.0f;
        switch (dirType)
        {
            case FGDirectionInputType.motion:
                if (InputDir.Count <= 1)
                    throw new Exception("Motions require at least 2 input directions");
                break;
            case FGDirectionInputType.charge:
                if (InputDir.Count != 2)
                    throw new Exception("Charge inputs must be from one direction to another");
                return GradeChargeInput(InputDir[0], InputDir[1]);
            case FGDirectionInputType.smash:
                if (InputDir.Count != 1)
                    throw new Exception("Cannot accept multiple input directions");
                break;
            case FGDirectionInputType.none:
                break;
        }

        float buttonGrade = 1.0f;
        switch (buttonType)
        {
            case FGButtonInputType.press:
                break;
            case FGButtonInputType.release:
                break;
            case FGButtonInputType.none:
                break;
        }
        FGInputGrade grade = FGInputGrade.Bad;
        if (dirGrade * buttonGrade > 0.7)
            grade = FGInputGrade.Excellent;
        else if (dirGrade * buttonGrade > 0.2)
            grade = FGInputGrade.Excellent;
        else
            grade = FGInputGrade.Excellent;
        throw new Exception("Implement proper input grading, propsoed grade is" + grade);
    }

    public int chargeDuration = 30;
    public int unchargeDuration = 10;
    public FGInputGrade GradeChargeInput(FGDirection dir1, FGDirection dir2)
    {
        int chargeReleaseFrame = 0;

        int totalChargeCounter = 0;
        int partialChargeCounter = 0;
        int lastPartialCharge = 0;
        int unchargeCounter = 0;
        bool hasDir2 = false;

        for (int i = inputBuffer.Count-1; i >= 0; i--)
        {
            if (!hasDir2 && inputBuffer[i].direction == dir2)
            {
                hasDir2 = true;
            }

            if (hasDir2)
            {
                if (inputBuffer[i].direction != dir2)
                {
                    chargeReleaseFrame = Mathf.Max(chargeReleaseFrame, i);
                }

                if (inputBuffer[i].direction == dir1)
                {
                    if (lastPartialCharge > chargeDuration)
                        totalChargeCounter = 0;

                    unchargeCounter = 0;
                    partialChargeCounter++;
                    totalChargeCounter++;
                }
                else
                {
                    if(partialChargeCounter > 0)
                    {
                        lastPartialCharge = partialChargeCounter;
                        partialChargeCounter = 0;
                    }

                    unchargeCounter++;
                    if (unchargeCounter > unchargeDuration)
                        return FGInputGrade.Bad;
                }
            }
        }

        int inputTimingGrade = inputBuffer.Count - chargeReleaseFrame;
        print(inputTimingGrade);

        if (totalChargeCounter < chargeDuration || inputTimingGrade > 20)
            return FGInputGrade.Bad;
        else if (chargeDuration <= totalChargeCounter && totalChargeCounter < chargeDuration + 10 && inputTimingGrade <= 10)
            return FGInputGrade.Excellent;
        else
            return FGInputGrade.Good;
    }


    public bool Detect236Input()
    {
        int inputStage = 6;
        for (int i = inputBuffer.Count - 1; i >= 0; i--)
        {
            if(inputStage == 6)
            {
                if (inputBuffer[i].direction == FGDirection.Dir6)
                    inputStage = 3;
            }
            else if (inputStage == 3)
            {
                if (inputBuffer[i].direction == FGDirection.Dir3)
                    inputStage = 2;
                if (inputBuffer[i].direction != FGDirection.Dir6 && inputBuffer[i].direction != FGDirection.Dir3)
                    inputStage = 6;
            }
            else if (inputStage == 2)
            {
                if (inputBuffer[i].direction == FGDirection.Dir2)
                    return true;
                if (inputBuffer[i].direction != FGDirection.Dir3 && inputBuffer[i].direction != FGDirection.Dir2)
                    inputStage = 6;
            }
        }
        return false;
    }
    public bool Detect214Input()
    {
        int inputStage = 4;
        for (int i = inputBuffer.Count - 1; i >= 0; i--)
        {
            if (inputStage == 4)
            {
                if (inputBuffer[i].direction == FGDirection.Dir4)
                    inputStage = 1;
            }
            else if (inputStage == 1)
            {
                if (inputBuffer[i].direction == FGDirection.Dir1)
                    inputStage = 2;
                if (inputBuffer[i].direction != FGDirection.Dir4 && inputBuffer[i].direction != FGDirection.Dir1)
                    inputStage = 4;
            }
            else if (inputStage == 2)
            {
                if (inputBuffer[i].direction == FGDirection.Dir2)
                    return true;
                if (inputBuffer[i].direction != FGDirection.Dir1 && inputBuffer[i].direction != FGDirection.Dir2)
                    inputStage = 4;
            }
        }
        return false;
    }
    public bool Detect623Input()
    {
        int inputStage = 3;
        for (int i = inputBuffer.Count - 1; i >= 0; i--)
        {
            if (inputStage == 3)
            {
                if (inputBuffer[i].direction == FGDirection.Dir3)
                    inputStage = 2;
            }
            else if (inputStage == 2)
            {
                if (inputBuffer[i].direction == FGDirection.Dir2)
                    inputStage = 6;
                if (inputBuffer[i].direction != FGDirection.Dir3 && inputBuffer[i].direction != FGDirection.Dir2)
                    inputStage = 3;
            }
            else if (inputStage == 6)
            {
                if (inputBuffer[i].direction == FGDirection.Dir6)
                    return true;
                if (inputBuffer[i].direction != FGDirection.Dir2 && inputBuffer[i].direction != FGDirection.Dir6)
                    inputStage = 3;
            }
        }
        return false;
    }

    public int inputWindow22 = 10;
    public bool Detect22Input()
    { 
        int inputStage = 2;
        for (int i = inputBuffer.Count - 1; i >= Mathf.Max(inputBuffer.Count - 1 - inputWindow22, 0); i--)
        {
            if (inputStage == 2)
            {
                if (inputBuffer[i].direction == FGDirection.Dir2)
                    inputStage = 5;
            }
            else if (inputStage == 5)
            {
                if (inputBuffer[i].direction != FGDirection.Dir3 && inputBuffer[i].direction != FGDirection.Dir2 && inputBuffer[i].direction != FGDirection.Dir1)
                    inputStage = 22;
            }
            else if (inputStage == 22)
            {
                if (inputBuffer[i].direction == FGDirection.Dir2)
                    return true;
            }
        }
        return false;
    }

    public bool Detect28ChargeInput()
    {
        return GradeChargeInput(FGDirection.Dir2, FGDirection.Dir8) != FGInputGrade.Bad;
    }

    public bool Detect46ChargeInput()
    {
        return GradeChargeInput(FGDirection.Dir4, FGDirection.Dir6) != FGInputGrade.Bad;
    }

    public HashSet<FGButton> GetButtonsPressed()
    {
        HashSet<FGButton> buttonsPressed = new HashSet<FGButton>();
        /*
if (Input.GetButton("A"))
    buttonsPressed.Add(FGButton.A);
if (Input.GetButton("B"))
    buttonsPressed.Add(FGButton.B);
if (Input.GetButton("C"))
    buttonsPressed.Add(FGButton.C);
if (Input.GetButton("D"))
    buttonsPressed.Add(FGButton.D);
*/
        return buttonsPressed;
    }

    public static FGDirection GetFGDirection(InputDirection direction, bool IsFacingLeft)
    {
        FGDirection fgDirection = FGDirection.Dir5;
        switch (direction)
        {
            case InputDirection.None:
                fgDirection = FGDirection.Dir5;
                break;
            case InputDirection.W:
                fgDirection = IsFacingLeft ? FGDirection.Dir4 : FGDirection.Dir6;
                break;
            case InputDirection.NW:
                fgDirection = IsFacingLeft ? FGDirection.Dir7 : FGDirection.Dir9;
                break;
            case InputDirection.N:
                fgDirection = FGDirection.Dir8;
                break;
            case InputDirection.NE:
                fgDirection = IsFacingLeft ? FGDirection.Dir9 : FGDirection.Dir7;
                break;
            case InputDirection.E:
                fgDirection = IsFacingLeft ? FGDirection.Dir6 : FGDirection.Dir4;
                break;
            case InputDirection.SE:
                fgDirection = IsFacingLeft ? FGDirection.Dir3 : FGDirection.Dir1;
                break;
            case InputDirection.S:
                fgDirection = FGDirection.Dir2;
                break;
            case InputDirection.SW:
                fgDirection = IsFacingLeft ? FGDirection.Dir1 : FGDirection.Dir3;
                break;
        }
        return fgDirection;
    }
}
