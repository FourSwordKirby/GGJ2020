using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialoguePromptTrigger : MonoBehaviour
{
    public TextAsset dialogue;
    public bool repeatingDialogue;
    public bool forceDialogueOnEnter = false;
    public bool forceBack;
    public Transform promptPosition;
    public Transform cameraPosition;


    public UnityEvent questEvent;

    private Vector3 triggerEnteredPosition;
    private bool dialogueActive;
    private SpeechAsset speechPrompt;

    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        triggerEnteredPosition = col.gameObject.transform.position;

        if (forceBack)
            col.attachedRigidbody.velocity =(col.transform.position - transform.position) * 2;

        if (forceDialogueOnEnter)
        {
            displayDialogue();
        }
        else
        {
            displayPrompt(promptPosition.position - triggerEnteredPosition);
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

        if (repeatingDialogue)
            GameManager.instance.StartConversation(dialogue, speakerPosition, cameraPosition, hideDialogue);
        else
            GameManager.instance.StartConversation(dialogue, speakerPosition, cameraPosition, destroy);
    }
    
    private void hideDialogue()
    {
        questEvent?.Invoke();
        dialogueActive = false;
        if(!forceDialogueOnEnter)
            displayPrompt(Vector3.zero);
    }

    private void displayPrompt(Vector3 triggerEnteredPosition)
    {
        Vector3 displacementVector = Vector3.Scale(triggerEnteredPosition, -Vector3.one);
        speechPrompt = UIController.displaySpeechPrompt(promptPosition.position, displacementVector);
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
        questEvent?.Invoke();
        Destroy(gameObject.transform.parent.gameObject);
    }
}
