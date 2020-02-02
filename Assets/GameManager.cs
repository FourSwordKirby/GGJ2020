using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void AfterDialogueEvent();

    public static GameManager instance;

    public void Awake()
    {
        if (GameManager.instance == null)
            instance = this;
        else if (this != instance)
            Destroy(this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartConversation(TextAsset dialogue, Vector3 speakerPosition, Transform cameraPosition = null, AfterDialogueEvent afterEvent = null)
    {
        StartCoroutine(PlayConversation(dialogue, speakerPosition, cameraPosition, afterEvent));
    }

    internal IEnumerator PlayConversation(TextAsset dialogue, Vector3 speakerPosition, Transform cameraPosition = null, AfterDialogueEvent afterEvent = null)
    {
        // If a special camera position was provided, tell the camera man to use it.
        CameraMan cameraMan = FindObjectOfType<CameraMan>();
        if (cameraPosition != null)
        {
            cameraMan.StartCinematicMode(cameraPosition);
        }

        ConversationPause();
        Vector3 currentSpeakerPosition = Vector3.zero;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<CharacterExperssion>().startTalking();

        List<string> dialogueLines = DialogueEngine.CreateDialogComponents(dialogue.text);
        DialogueUIController.instance.init(dialogueLines.Count);

        Dictionary<string, GameObject> speakerDict = DialogueEngine.GetSpeakers(dialogue.text).ToDictionary(x => x, x => GameObject.Find(x));

        int lineTracker = 0;
        while(lineTracker < dialogueLines.Count)
        {
            string currentLine = dialogueLines[lineTracker];

            if (currentLine.StartsWith("[expression]"))
            {
                print(currentLine);
                var expressionString = currentLine.Split(' ')[1];
                var expression = (CharacterExperssion.Expressions)Enum.Parse(typeof(CharacterExperssion.Expressions), expressionString);
                player.GetComponent<CharacterExperssion>().changeExpression(expression);
            }
            else
            {
                string speaker = DialogueEngine.GetSpeaker(currentLine);

                if (speaker != "")
                    speakerPosition = speakerDict[speaker].transform.position;
                else
                    speakerPosition = currentSpeakerPosition;

                currentSpeakerPosition = speakerPosition;
                DialogueUIController.instance.displaySpeechBubble(currentLine, currentSpeakerPosition);
                while (!DialogueUIController.instance.ready)
                    yield return null;
                while (!Controls.confirmInputDown())
                    yield return null;
            }

            lineTracker++;
        }
        DialogueUIController.instance.finishDialogue();
        player.GetComponent<CharacterExperssion>().stopTalking();

        while(!DialogueUIController.instance.ready)
            yield return null;

        ConversationUnpause();

        cameraMan.EndCinematicMode();

        afterEvent();
        yield return null;
    }

    //hacky gameplay pause implementation because hackathon
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
