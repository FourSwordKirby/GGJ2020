using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualNovelCharacterUI : MonoBehaviour {

    //Used by the parser to determine which character to display
    public string characterName;
    public List<Sprite> Sprites;

    public Image spriteRenderer;
    public bool inScene;
    public StageSide side;

    public void Awake()
    {
        this.spriteRenderer = this.GetComponent<Image>();
    }

    public enum StageSide
    {
        Left,
        Right
    }
}
