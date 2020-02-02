using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePromptTrigger : MonoBehaviour
{
    public TextAsset dialogue;
    public bool repeatingDialogue;

    private bool dialogueActive;

    private SpeechAsset speechPrompt;

    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        displayPrompt();
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
        if (repeatingDialogue)
            GameManager.instance.StartConversation(dialogue, transform.position, hideDialogue);
        else
            GameManager.instance.StartConversation(dialogue, transform.position, destroy);
    }
    
    private void hideDialogue()
    {
        dialogueActive = false;
        displayPrompt();
    }

    private void displayPrompt()
    {
        speechPrompt = UIController.displaySpeechPrompt(transform.position);
    }


    private void hidePrompt()
    {
        UIController.hideSpeechPrompt(speechPrompt);
    }

    private void destroy()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
