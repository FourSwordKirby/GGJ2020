using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
}
