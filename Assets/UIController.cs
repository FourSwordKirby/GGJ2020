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

    public static IDialogueBubble DisplaySpeechPrompt(Vector3 speakerPosition, Vector2 displacementVector)
    {
        IDialogueBubble speechPrompt = Instantiate(staticSpeechPromptPrefab).GetComponent<IDialogueBubble>();
        speechPrompt.DeployAt(speakerPosition, displacementVector);

        return speechPrompt;
    }

    public static void HideSpeechPrompt(IDialogueBubble speechPrompt)
    {
        speechPrompt.Hide();
    }

    public static void DeploySpeechBubbleAt(DialogueBubble speechBubble, Vector3 speakerPosition, Vector2 displacementVector)
    {
        speechBubble.gameObject.SetActive(true);
        speechBubble.DeployAt(speakerPosition, displacementVector);
    }

    public static void DeploySpeechBubble(IDialogueBubble speechBubble)
    {
        speechBubble.Show();
    }

    public static DialogueBubble GenerateSpeechBubblePrefab()
    {
        DialogueBubble speechBubble = Instantiate(staticSpeechBubblePrefab).GetComponent<DialogueBubble>();
        speechBubble.gameObject.SetActive(false);
        return speechBubble;
    }

    public static void HideSpeechBubble(IDialogueBubble speechBubble)
    {
        Debug.Log("hide");
        speechBubble.Blur();
        speechBubble.Hide();
    }
}
