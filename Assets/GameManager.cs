using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void AfterDialogueEvent();

    public const float DIALOGUE_DISPLAY_COOLDOWN = 0.2f;

    public static GameManager instance;
    public void Awake()
    {
        if (UIController.instance == null)
            instance = this;
        else if (this != instance)
            Destroy(this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartConversation(TextAsset dialogue, Vector3 speakerPosition, AfterDialogueEvent afterEvent = null)
    {
        StartCoroutine(PlayConversation(dialogue, speakerPosition, afterEvent));
    }

    internal IEnumerator PlayConversation(TextAsset dialogue, Vector3 speakerPosition, AfterDialogueEvent afterEvent = null)
    {
        ConversationPause();

        List<string> dialogueLines = DialogEngine.CreateDialogComponents(dialogue.text);
        int lineTracker = 0;
        while(lineTracker < dialogueLines.Count)
        {
            string currentLine = dialogueLines[lineTracker];
            SpeechAsset speechBubble = UIController.displaySpeechBubble(currentLine, speakerPosition);

            yield return new WaitForSeconds(DIALOGUE_DISPLAY_COOLDOWN);
            while (!Controls.confirmInputDown())
                yield return null;

            print(speechBubble);
            UIController.hideSpeechBubble(speechBubble);
            lineTracker++;
        }

        afterEvent();
        ConversationUnpause();
        yield return null;
    }

    //hacky gameplay pause implementation for speed
    private void ConversationPause()
    {
        PauseGameplay();
    }
    private void ConversationUnpause()
    {
        ResumeGameplay();
    }

    bool gamePaused = false;
    public bool Paused { get { return gamePaused; } }
    public void PauseGameplay()
    {
        gamePaused = true;
        foreach (CharacterMovement entity in GameObject.FindObjectsOfType<CharacterMovement>())
            entity.enabled = false;
    }
    public void ResumeGameplay()
    {
        gamePaused = false;
        foreach (CharacterMovement entity in GameObject.FindObjectsOfType<CharacterMovement>())
            entity.enabled = true;
    }

}
