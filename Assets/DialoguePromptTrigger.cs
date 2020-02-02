using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePromptTrigger : MonoBehaviour
{
    public TextAsset dialogue;
    public bool repeatingDialogue;
    public bool forceDialogueOnEnter = false;
    public Transform desiredSpeaker;
    public Transform cameraPosition;

    private bool dialogueActive;

    private SpeechAsset speechPrompt;

    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        if (forceDialogueOnEnter)
        {
            displayDialogue();

        }
        else
        {
            displayPrompt();
        }
    }

    void OnTriggerExit(Collider col)
    {
        hidePrompt();
    }

    void OnTriggerStay(Collider col)
    {
        if(Controls.confirmInputDown() && !dialogueActive)
        {
            displayDialogue();
        }
    }

    private void displayDialogue()
    {
        dialogueActive = true;
        hidePrompt();

        Vector3 speakerPosition = transform.position;
        if (desiredSpeaker != null)
        {
            // Add 1 unit up so that the speech bubble is in a reasonable spot.
            speakerPosition = desiredSpeaker.position + Vector3.up * 1f;
        }

        if (repeatingDialogue)
            GameManager.instance.StartConversation(dialogue, speakerPosition, cameraPosition, hideDialogue);
        else
            GameManager.instance.StartConversation(dialogue, speakerPosition, cameraPosition, destroy);
    }
    
    private void hideDialogue()
    {
        dialogueActive = false;
        displayPrompt();
    }

    private void displayPrompt()
    {
        Vector2 speakerScrenPosition = Camera.main.WorldToScreenPoint(transform.position);

        bool onLeftSide;
        if (speakerScrenPosition.x < Camera.main.pixelWidth / 2.0f)
            onLeftSide = false;
        else
            onLeftSide = true;

        speechPrompt = UIController.displaySpeechPrompt(transform.position, onLeftSide);
    }


    private void hidePrompt()
    {
        if (speechPrompt == null)
        {
            return;
        }

        UIController.hideSpeechPrompt(speechPrompt);
    }

    private void destroy()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
