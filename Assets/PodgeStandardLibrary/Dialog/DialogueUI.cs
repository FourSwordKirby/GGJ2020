using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;
using System;

public class DialogueUI : MonoBehaviour
{
    public Text dialogField;
    public Text speaker;
    public Image dialogBox;
    public Image speakerBox;

    public bool dialogCompleted;

    public string dialog = "";

    private float textDisplaySpeed;
    private float textDisplayTimer;

    private const float FAST_DISPLAY_SPEED = 0.0f;
    private const float SLOW_DISPLAY_SPEED = 0.03f;

    private AudioClip textSfx;

    // Use this for initialization
    void Awake()
    {
        dialogField.text = "";
        speaker.text = "";
        dialogCompleted = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Skip dialog if confirm button is pressed again
        if (Controls.dialogAdvanceDown())
        {
            setSpeed(DisplaySpeed.immediate);
        }


        //Do something where text appears according to the textDisplaySpeed
        if (textDisplayTimer > 0)
        {
            textDisplayTimer -= UIController.deltaTime;
            return;
        }

        if (this.dialogField.text != dialog)
        {
            //Screw the scrolling dialog, going back to single display for now
            /*
            char[] dialogCharArray = new char[dialog.Length];
            dialogCharArray = this.dialogField.text.ToCharArray();
            dialogCharArray[dialogTracker] = dialog[dialogTracker];
            this.dialogField.text = new string(dialogCharArray);
            dialogTracker++;
            */

            dialogField.text = dialog;
            //This can be pretty jank, probably need to make an audio source onthe camera or something e-e
            //AudioSource.PlayClipAtPoint(textSfx, CameraControls.instance.transform.position);

            textDisplayTimer = textDisplaySpeed;
            dialogCompleted = false;
        }
        else
        {
            dialogCompleted = true;
            //Voices.StopVoice();
        }
    }

    public void displayDialog(string dialog, string speaker = "", AudioClip sfx = null, DisplaySpeed displaySpeed = DisplaySpeed.fast)
    {
        //SetDialogSound(sfx);
        //Fix this up later bc it's causing errors atm
        //Voices.StartVoice(0, dialog);

        //print("Dialog to display: " + dialog);

        dialogCompleted = false;

        this.gameObject.SetActive(true);
        this.speaker.text = speaker;
        this.dialog = dialog;

        string taggedText = "";
        for (int i = 0; i < dialog.Length; i++)
        {
            if (dialog[i] == '<')
            {
                string insertTag = "";
                while (dialog[i] != '>')
                {
                    insertTag += dialog[i];
                    i++;
                }
                insertTag += dialog[i];
                taggedText += insertTag;
            }
            else
            {
                taggedText += " ";
            }
        }
        //Prevents the name from flickering
        this.dialogField.text = taggedText;


        setSpeed (displaySpeed);
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }

    public void resolveDialog()
    {
        this.dialogField.text = dialog;
    }

    public void setSpeed(DisplaySpeed displaySpeed) {
        if (displaySpeed == DisplaySpeed.immediate)
        {
            this.dialogField.text = dialog;
        }
        else if (displaySpeed == DisplaySpeed.fast)
        {
            textDisplaySpeed = FAST_DISPLAY_SPEED;
        }
        else if (displaySpeed == DisplaySpeed.slow)
        {
            textDisplaySpeed = SLOW_DISPLAY_SPEED;
        }
    }
}

public enum DisplaySpeed
{
    immediate,
    slow,
    fast
}