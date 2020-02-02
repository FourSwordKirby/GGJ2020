using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DialogueEngine : MonoBehaviour
{
    public static DialogueEngine instance;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else if (this != instance)
            Destroy(this.gameObject);
    }

    public static List<string> CreateDialogComponents(string text)
    {
        List<string> dialogComponents = new List<string>(text.Split('\n'));
        dialogComponents = dialogComponents.Select(x => x.Trim()).ToList();
        dialogComponents = dialogComponents.Where(x => x != "").ToList();
        return dialogComponents;
    }

    internal static List<string> GetSpeakers(string text)
    {
        List<string> speakers = new List<string>();

        List<string> dialogComponents = CreateDialogComponents(text);
        foreach (string line in dialogComponents)
        {
            string speaker = GetSpeaker(line);
            if(speaker != "")
                speakers.Add(speaker);
        }
        return speakers;
    }

    public static string GetSpeaker(string line)
    {
        string[] dialoguePieces = line.Split(':');

        if (dialoguePieces.Length > 1)
            return dialoguePieces[0];
        else
            return "";
    }
}
