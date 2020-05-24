using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventQueue : MonoBehaviour
{

    private static Queue<GameEvent> eventQueue = new Queue<GameEvent>();

    //Instance Managing;
    public static GameEventQueue instance;
    public void Awake()
    {
        if (GameEventQueue.instance == null)
            instance = this;
        else if (this != instance)
            Destroy(this.gameObject);
    }


    public void AddGameEvent(GameEvent gameEvent)
    {
        eventQueue.Enqueue(gameEvent);
        if (!queueRunning)
            StartCoroutine(StartQueue());
    }

    public void StopAllEvents()
    {
        queueRunning = false;
    }

    static bool queueRunning = false;
    static bool runningEvent = false;
    IEnumerator StartQueue()
    {
        queueRunning = true;
        while (queueRunning)
        {
            if (eventQueue.Count != 0)
            {
                if (!runningEvent)
                {
                    GameEvent gameEvent = eventQueue.Dequeue();
                    gameEvent.RunEvent();
                    while (!gameEvent.EventCompleted())
                        yield return null;
                }
            }
            yield return null;
        }

        eventQueue.Clear();
    }
}
