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
        for (int i = 0; i < speechBubbles.Count; i++)
            speechBubbles[i].hide();
        StartCoroutine(cleanup());
    }

    private IEnumerator cleanup()
    {
        yield return new WaitForSeconds(5.0f);

        for (int i = 0; i < speechBubbles.Count; i++)
            speechBubbles[i].destroy();
    }

    public void displaySpeechBubble(string text, Vector3 speakerPosition)
    {
        ready = false;
        StartCoroutine(animateLogs(text, speakerPosition));
    }

    IEnumerator animateLogs(string text, Vector3 speakerPosition)
    {
        //crappy concurrency lol
        SpeechAsset speechBubble = UIController.displaySpeechBubble(text, speakerPosition);
        speechBubble.focus();

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
                speechBubbles[i].blur();
        }

        speechBubbles.Add(speechBubble);

        speechBubbleTracker = speechBubbles.Count;

        yield return new WaitForSeconds(0.2f);

        ready = true;
    }
}
