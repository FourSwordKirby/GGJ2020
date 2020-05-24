using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceUI : MonoBehaviour {

    public List<Image> choices;
    public int currentChoice;

    public bool choiceMade;

	// Update is called once per frame
	void Update () {
        for(int i = 0; i < choices.Count; i++)
        {
            if(i == currentChoice)
                choices[i].color = Color.grey;
            else
                choices[i].color = Color.white;
        }
    }

    public void OpenChoices()
    {
        this.gameObject.SetActive(true);
        this.choiceMade = false;
    }

    public void CloseChoices()
    {
        this.gameObject.SetActive(false);
    }
}
