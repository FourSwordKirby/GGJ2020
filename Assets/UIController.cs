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

    public static SpeechAsset deploySpeechPrompt(Vector3 speakerPosition)
    {
        SpeechAsset speechPrompt = Instantiate(staticSpeechPromptPrefab).GetComponent<SpeechAsset>();
        speechPrompt.deployAt(speakerPosition);

        return speechPrompt;
    }

    public static void hideSpeechPrompt(SpeechAsset speechPrompt)
    {
        speechPrompt.hide();
    }

    public static void deploySpeechAsset(Vector3 speakerPosition, string text)
    {
        SpeechAsset speechAsset = Instantiate(staticSpeechBubblePrefab).GetComponent<SpeechAsset>();
        speechAsset.setText(text);
        speechAsset.deployAt(speakerPosition);
    }
}
