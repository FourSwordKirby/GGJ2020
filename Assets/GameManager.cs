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
        Dialogue processedDialogue = new Dialogue(dialogue.text);
        StartCoroutine(PlayConversation(processedDialogue, cameraPosition, afterEvent));
    }

    internal IEnumerator PlayConversation(Dialogue dialogue, Transform cameraPosition = null, AfterDialogueEvent afterEvent = null)
    {
        // If a special camera position was provided, tell the camera man to use it.
        ConversationPause();
        CameraMan cameraMan = FindObjectOfType<CameraMan>();
        if (cameraPosition != null)
            cameraMan.StartCinematicMode(cameraPosition);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<CharacterDialogueAnimator>().startTalking();

        DialogueUIController.instance.init(dialogue);

        //Dictionary<string, DialogueAnimator> speakerDict = DialogueEngine.GetSpeakers(dialogue.text).ToDictionary(x => x, x => GameObject.Find(x).GetComponent<DialogueAnimator>());

        int lineTracker = 0;
        //string currentSpeaker = "";
        while (!dialogue.IsFinished)
        {
            ScriptLine line = dialogue.GetNextLine();
            line.PerformLine();
            while (!line.IsFinished())
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    lineTracker--;
                    StartCoroutine(DialogueUIController.instance.animateLogs(lineTracker));
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    lineTracker++;
                    StartCoroutine(DialogueUIController.instance.animateLogs(lineTracker));
                }
                yield return null;
            }
            lineTracker++;
            /*
            string currentLine = dialogueLines[lineTracker];
            if (currentLine.StartsWith("[expression]"))
            {
                print(currentLine);
                var expressionString = currentLine.Split(' ')[1];
                var expression = (CharacterExpressionAnimator.Expressions)Enum.Parse(typeof(CharacterExpressionAnimator.Expressions), expressionString);
                player.GetComponent<CharacterDialogueAnimator>().changeExpression(expression);
            }
            else
            {
                string speaker = DialogueEngine.GetSpeaker(currentLine);
                string spokenLine = DialogueEngine.GetSpokenLine(currentLine);
                if (speaker != "")
                    currentSpeaker = speaker;
                else if (currentSpeaker == "")
                    Debug.LogWarning("Speaker not specified");
                Vector3 speakerPosition = speakerDict[currentSpeaker].getSpeechOrigin();

                DialogueUIController.instance.displaySpeechBubble(spokenLine, speakerPosition);
                while (!DialogueUIController.instance.ready)
                    yield return null;
            }
            lineTracker++;
            */
        }
        DialogueUIController.instance.finishDialogue();
        player.GetComponent<CharacterDialogueAnimator>().stopTalking();

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
