﻿using UnityEngine;
using System.Collections;

public abstract class Interactable : MonoBehaviour {
    public abstract void Interact();

    void OnTriggerStay2D(Collider2D col)
    {
        /*
        Player player = col.gameObject.GetComponent<Player>();
        if (player != null)
        {
            //if (player.currentInteractable != this)
            //{
            //    player.currentInteractable = this;
            //    StartCoroutine(player.ShowSymbol(Symbol.Interested));
            //}
        }
        */
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        /*
        Player player = col.gameObject.GetComponent<Player>();
        if (player != null)
        {
            //if (player.currentInteractable == this)
            //{
            //    player.currentInteractable = null;
            //    StartCoroutine(player.HideSymbol());
            //}
        }
        */
    }
}
