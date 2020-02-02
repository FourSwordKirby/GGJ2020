using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static bool visitedBridge;
    public static bool pickedUpRockChunk;

    public static QuestManager instance;
    public void Awake()
    {
        if (instance == null)
            instance = this;
        else if (this != instance)
            Destroy(this.gameObject);
    }
}
