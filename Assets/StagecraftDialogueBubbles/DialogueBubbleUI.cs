using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DialogueBubbleUI : MonoBehaviour
{
    private List<DialogueBubble> speechBubbles = new List<DialogueBubble>();
    private int onScreenSpeechBubbleLimit = 2;
    private int currentLineNumber = 0;
    private int z_offset = 1;

    public Dialogue activeDialogue;
    public bool ready;

    private List<float> randomSeedsX;
    private List<float> randomSeedsY;

    public static DialogueBubbleUI instance;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else if (this != instance)
            Destroy(this.gameObject);
    }

    //This hacky implementation is kind of bad but I really don't want to mess with making the animator do this
    public void init(Dialogue dialogue)
    {
        currentLineNumber = dialogue.currentPosition;
        activeDialogue = dialogue;
        int speakingLineCount = activeDialogue.speakingLineCount;

        speechBubbles = new List<DialogueBubble>(speakingLineCount);
        randomSeedsX = new List<float>(speakingLineCount);
        randomSeedsY = new List<float>(speakingLineCount);

        for (int i = 0; i < speakingLineCount; i++)
        {
            DialogueBubble speechBubble = UIController.GenerateSpeechBubblePrefab();
            speechBubbles.Add(speechBubble);
            randomSeedsX.Add(Random.Range(-1.0f, 1.0f));
            randomSeedsY.Add(Random.Range(0f, 1.0f));
        }
    }

    public void finishDialogue()
    {
        ready = false;
        for (int i = 0; i < speechBubbles.Count; i++)
            speechBubbles[i].Hide();
        StartCoroutine(cleanup());
    }

    private IEnumerator cleanup()
    {
        yield return new WaitForSeconds(1.0f);
        for (int i = 0; i < speechBubbles.Count; i++)
            speechBubbles[i].Destroy();
        ready = true;
    }

    public Camera dialogueCamera;
    //Displays the a speech bubble according to its text and position in the overall dialogue
    public void DisplaySpeechBubble(SpeakingLineContent speakingLineContent, Vector3 speakerPosition)
    {
        int lineNumber = speakingLineContent.lineNumber;

        ready = false;
        Vector2 speakerScreenPosition = dialogueCamera.WorldToScreenPoint(speakerPosition);

        float relativeXdisplacment = (Camera.main.pixelWidth / 2.0f - speakerScreenPosition.x) / Camera.main.pixelWidth;
        float relativeYdisplacment = (Camera.main.pixelHeight / 2.0f - speakerScreenPosition.y) / Camera.main.pixelHeight + 0.1f; // The dialogue should always be in the upper portion of the screen 

        Vector2 displacementVector = new Vector2(relativeXdisplacment, relativeYdisplacment);

        // *(offscreen dialogue we will need to handle seperately)
        DialogueBubble speechBubble = speechBubbles[lineNumber];
        speechBubble.SetDialogueBubbleContent(speakingLineContent);
        UIController.DeploySpeechBubbleAt(speechBubble, speakerPosition, displacementVector);

        StartCoroutine(animateLogs(lineNumber));
    }


    //Automatically animates the logs to the current state of the underlying dialogue data structure
    public IEnumerator animateLogs(int targetLineNumber)
    {
        Debug.Log(targetLineNumber);
        Debug.Log(currentLineNumber);
        if (currentLineNumber != targetLineNumber)
        {
            int offset = targetLineNumber - currentLineNumber;
            
            //crappy concurrency lol
            float logTweenTime = 0.2f;
            float delta = z_offset / logTweenTime * Time.deltaTime;

            while (logTweenTime > 0)
            {
                logTweenTime -= Time.deltaTime;
                for (int i = 0; i < speechBubbles.Count; i++)
                {
                    DialogueBubble animatedSpeechBubble = speechBubbles[i];
                    if(offset > 0)
                    {
                        if (targetLineNumber - offset <= i && i < targetLineNumber)
                            animatedSpeechBubble.transform.position += (Vector3.right * randomSeedsX[i] + Vector3.up * randomSeedsY[i]) * delta;
                    }
                    else
                    {
                        if (targetLineNumber <= i && i < targetLineNumber - offset)
                            animatedSpeechBubble.transform.position -= (Vector3.right * randomSeedsX[i] + Vector3.up * randomSeedsY[i]) * delta;
                    }
                    if (targetLineNumber - onScreenSpeechBubbleLimit < i && i <= targetLineNumber)
                        UIController.DeploySpeechBubble(animatedSpeechBubble);
                    if (i <= targetLineNumber - onScreenSpeechBubbleLimit)
                        UIController.HideSpeechBubble(animatedSpeechBubble);
                    if (i > targetLineNumber)
                        UIController.HideSpeechBubble(animatedSpeechBubble);
                    if (i == targetLineNumber)
                        animatedSpeechBubble.Focus();
                    int currentPosition = Mathf.Clamp(currentLineNumber - i, 0, onScreenSpeechBubbleLimit);
                    int targetPosition = Mathf.Clamp(targetLineNumber - i, 0, onScreenSpeechBubbleLimit);
                    animatedSpeechBubble.transform.position += (targetPosition - currentPosition) * Vector3.forward * delta;
                }
                yield return null;
            }
            for (int i = 0; i < speechBubbles.Count; i++)
            {
                if (i != targetLineNumber)
                    speechBubbles[i].Blur();
            }
            currentLineNumber = targetLineNumber;
        }
        yield return new WaitForSeconds(0.2f);
        while (!Controls.confirmInputDown())
            yield return null;
        ready = true;
    }
}
