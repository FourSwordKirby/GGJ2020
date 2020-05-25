using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualNovelCharacterController : MonoBehaviour {

    public List<VisualNovelCharacterUI> loadedCharacters;

    public GameObject leftWingPosition;
    public GameObject rightWingPosition;

    public GameObject leftPosition;
    public GameObject rightPosition;

    public void Enter(string characterName, Direction dir)
    {
        VisualNovelCharacterUI portrait = loadedCharacters.Find(x => x.characterName == characterName);
        if (dir == Direction.Left)
            portrait.side = VisualNovelCharacterUI.StageSide.Left;
        if (dir == Direction.Right)
            portrait.side = VisualNovelCharacterUI.StageSide.Right;

        if (dir == Direction.Left)
            StartCoroutine(TweenInCharacter(portrait, leftWingPosition.transform.position, leftPosition.transform.position));
        if (dir == Direction.Right)
            StartCoroutine(TweenInCharacter(portrait, rightWingPosition.transform.position, rightPosition.transform.position));
    }

    public void Exit(string characterName, Direction dir)
    {
        VisualNovelCharacterUI portrait = loadedCharacters.Find(x => x.characterName == characterName);
        if (dir == Direction.Left)
            portrait.side = VisualNovelCharacterUI.StageSide.Left;
        if (dir == Direction.Right)
            portrait.side = VisualNovelCharacterUI.StageSide.Right;

        if (dir == Direction.Left)
            StartCoroutine(TweenOutCharacter(portrait, leftPosition.transform.position, leftWingPosition.transform.position));
        if (dir == Direction.Right)
            StartCoroutine(TweenOutCharacter(portrait, rightPosition.transform.position, rightWingPosition.transform.position));
    }


    //Janky library functions
    public IEnumerator TweenInCharacter(VisualNovelCharacterUI portrait, Vector3 FromPosition, Vector3 ToPosition, float tweenLength = 1.0f)
    {
        float counter = 0;

        portrait.transform.position = FromPosition;
        while(counter < tweenLength)
        {
            counter += DialogueUIController.deltaTime;
            portrait.spriteRenderer.color = Color.Lerp(Color.clear, Color.white, counter / tweenLength);
            portrait.transform.position = Vector3.Lerp(FromPosition, ToPosition, counter / tweenLength);
            yield return null;
        }
        portrait.inScene = true;
        yield return null;
    }

    public IEnumerator TweenOutCharacter(VisualNovelCharacterUI portrait, Vector3 FromPosition, Vector3 ToPosition, float tweenLength = 1.0f)
    {
        portrait.inScene = false;
        float counter = 0;

        portrait.transform.position = FromPosition;
        while (counter < tweenLength)
        {
            counter += DialogueUIController.deltaTime;
            portrait.spriteRenderer.color = Color.Lerp(Color.white, Color.clear, counter / tweenLength);
            portrait.transform.position = Vector3.Lerp(FromPosition, ToPosition, counter / tweenLength);
            yield return null;
        }
        yield return null;
    }

    internal void ChangeSprite(string characterName, int spriteIdx)
    {
        VisualNovelCharacterUI portrait = loadedCharacters.Find(x => x.characterName == characterName);
        portrait.spriteRenderer.sprite = portrait.Sprites[spriteIdx];
    }

    public void SetSpeaker(string characterName)
    {
        foreach(VisualNovelCharacterUI portrait in loadedCharacters)
        {
            if(portrait.inScene)
            {
                if (portrait.characterName == characterName)
                    SendForward(portrait);
                else
                    SendBackward(portrait);
            }
        }
    }

    private void SendForward(VisualNovelCharacterUI portrait)
    {
        StartCoroutine(FadeInCharacter(portrait, 0.5f));
    }

    private void SendBackward(VisualNovelCharacterUI portrait)
    {
        StartCoroutine(FadeOutCharacter(portrait, 0.5f));
    }

    public IEnumerator FadeInCharacter(VisualNovelCharacterUI portrait, float tweenLength = 1.0f)
    {
        float counter = 0;

        while (counter < tweenLength)
        {
            counter += DialogueUIController.deltaTime;
            portrait.spriteRenderer.color = Color.Lerp(portrait.spriteRenderer.color, Color.white, counter / tweenLength);
            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
    }

    public IEnumerator FadeOutCharacter(VisualNovelCharacterUI portrait, float tweenLength = 1.0f)
    {
        float counter = 0;
        
        while (counter < tweenLength)
        {
            counter += DialogueUIController.deltaTime;
            portrait.spriteRenderer.color = Color.Lerp(portrait.spriteRenderer.color, Color.white * 0.7f, counter / tweenLength);
            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
    }

    public void ExitAllCharacters()
    {
        foreach (VisualNovelCharacterUI c in loadedCharacters)
        {
            if (c.inScene)
            {
                if (c.side == VisualNovelCharacterUI.StageSide.Left)
                    Exit(c.characterName, Direction.Left);
                if (c.side == VisualNovelCharacterUI.StageSide.Right)
                    Exit(c.characterName, Direction.Right);
            }
        }
    }

    public void Open()
    {
        this.gameObject.SetActive(true);
    }

    public void Close()
    {
        ExitAllCharacters();
    }

    public enum Direction
    {
        Left,
        Right
    }

}
