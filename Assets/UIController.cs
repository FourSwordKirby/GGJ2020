using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject speechBubblePrefab;
    public GameObject speechPromptPrefab;

    public static GameObject staticSpeechBubblePrefab;
    public static GameObject staticSpeechPromptPrefab;

    public static UIController instance;
    internal static float deltaTime = 1.0f/60.0f;

    public void Awake()
    {
        if (UIController.instance == null)
            instance = this;
        else if (this != instance)
            Destroy(this.gameObject);

        staticSpeechBubblePrefab = speechBubblePrefab;
        staticSpeechPromptPrefab = speechPromptPrefab;
    }

    public static SpeechAsset displaySpeechPrompt(Vector3 speakerPosition)
    {
        SpeechAsset speechPrompt = Instantiate(staticSpeechPromptPrefab).GetComponent<SpeechAsset>();
        speechPrompt.deployAt(speakerPosition);

        return speechPrompt;
    }

    public static void hideSpeechPrompt(SpeechAsset speechPrompt)
    {
        speechPrompt.hide();
    }

    public static SpeechAsset displaySpeechBubble(string text, Vector3 speakerPosition)
    {
        SpeechAsset speechBubble = Instantiate(staticSpeechBubblePrefab).GetComponent<SpeechAsset>();
        speechBubble.setText(text);
        speechBubble.deployAt(speakerPosition);

        return speechBubble;
    }

    public static void hideSpeechBubble(SpeechAsset speechBubble)
    {
        speechBubble.blur();
        speechBubble.hide();
    }
}
