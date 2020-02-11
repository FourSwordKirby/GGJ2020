using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DialogueUIController : MonoBehaviour
{
    private List<SpeechAsset> speechBubbles = new List<SpeechAsset>();
    private int onScreenSpeechBubbleLimit = 5;
    private int speechBubbleTracker = 0;
    private int z_offset = 1;

    public bool ready;

    private List<float> randomSeedsX;
    private List<float> randomSeedsY;

    public static DialogueUIController instance;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else if (this != instance)
            Destroy(this.gameObject);
    }

    //This hacky implementation is kind of bad but I really don't want to mess with making the animator do this
    public void init(int maxLogLength)
    {
        speechBubbles = new List<SpeechAsset>();

        randomSeedsX = new List<float>(maxLogLength);
        randomSeedsY = new List<float>(maxLogLength);

        for (int i = 0; i < maxLogLength; i++)
        {
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

    //We need to wait for the camera to get into position so that we can determine where to put the speech bubble
    //This approach is flawed as we should ideally know where the speech bubbles should go given the position of the camera and the actors. 
    //In the future we should be able to do this check without having to wait for the camera to get into position
    public Camera dialogueCamera;
    public void displaySpeechBubble(string text, Vector3 speakerPosition)
    {
        ready = false;
        Vector2 speakerScrenPosition = dialogueCamera.WorldToScreenPoint(speakerPosition);

        float relativeXdisplacment = (Camera.main.pixelWidth / 2.0f - speakerScrenPosition.x) / Camera.main.pixelWidth;
        float relativeYdisplacment = (Camera.main.pixelHeight / 2.0f - speakerScrenPosition.y) / Camera.main.pixelHeight + 0.1f; // The dialogue should always be in the upper portion of the screen 
                                                                                                                                  // *(offscreen dialogue we will need to handle seperately)
        StartCoroutine(animateLogs(text, speakerPosition, new Vector2(relativeXdisplacment, relativeYdisplacment)));
    }


    IEnumerator animateLogs(string text, Vector3 speakerPosition, Vector2 displacementVector)
    {
        //crappy concurrency lol
        SpeechAsset speechBubble = UIController.displaySpeechBubble(text, speakerPosition, displacementVector);
        speechBubble.Focus();

        float logTweenTime = 0.2f;
        float delta = z_offset / logTweenTime * Time.deltaTime;

        if (speechBubbles.Count > 0)
        {
            while (logTweenTime > 0)
            {
                logTweenTime -= Time.deltaTime;
                for (int i = 0; i < speechBubbles.Count; i++)
                {
                    SpeechAsset prevSpeechBubble = speechBubbles[i];
                    if (i == speechBubbles.Count - 1)
                        prevSpeechBubble.transform.position += (Vector3.forward + Vector3.right * randomSeedsX[i] + Vector3.up * randomSeedsY[i]) * delta;
                    else if (i > speechBubbles.Count - onScreenSpeechBubbleLimit)
                        prevSpeechBubble.transform.position += Vector3.forward * delta;
                    else
                    {
                        prevSpeechBubble.transform.position += Vector3.forward * delta;
                        UIController.hideSpeechBubble(prevSpeechBubble);
                    }
                }
                yield return null;
            }
            for (int i = 0; i < speechBubbles.Count; i++)
                speechBubbles[i].Blur();
        }

        speechBubbles.Add(speechBubble);
        speechBubbleTracker = speechBubbles.Count;

        yield return new WaitForSeconds(0.2f);

        ready = true;
    }
}
