using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePromptTrigger : MonoBehaviour
{
    public TextAsset dialogue;
    public SpeechAsset speechPrompt;

    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        speechPrompt = UIController.deploySpeechPrompt(this.transform.position);
    }

    void OnTriggerExit(Collider col)
    {
        UIController.hideSpeechPrompt(speechPrompt);
    }

    private void OnTriggerStay(Collider col)
    {
        if(Controls.confirmInputDown())
        {
            StartCoroutine(GameManager.instance.StartConversation(dialogue));
        }
    }
}
