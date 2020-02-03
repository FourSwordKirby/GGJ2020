using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public GameObject RabbitReminderTrigger;
    public GameObject BridgeReminderTrigger;
    public GameObject NoRockPickupTrigger;
    public GameObject RockPickupTrigger;
    public GameObject DestroyStatueTrigger;

    public static QuestManager instance;
    public void Awake()
    {
        if (instance == null)
            instance = this;
        else if (this != instance)
            Destroy(this.gameObject);

        RabbitReminderTrigger.SetActive(true);
        BridgeReminderTrigger.SetActive(false);
        NoRockPickupTrigger.SetActive(true);
        RockPickupTrigger.SetActive(false);
        DestroyStatueTrigger.SetActive(false);
    }

    public void PickUpRabbit()
    {
        Destroy(RabbitReminderTrigger);
        BridgeReminderTrigger.SetActive(true);
    }

    public void VisitBridge()
    {
        Destroy(BridgeReminderTrigger);
    }

    public void talkToStatue()
    {
        Destroy(NoRockPickupTrigger);
        RockPickupTrigger.SetActive(true);
    }

    public void PickUpRock()
    {
        DestroyStatueTrigger.SetActive(true);
    }
}
