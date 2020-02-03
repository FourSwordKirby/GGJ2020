using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public bool EnableStartEvent = false;

    public GameObject RabbitReminderTrigger;
    public GameObject BridgeReminderTrigger;
    public GameObject NoRockPickupTrigger;
    public GameObject RockPickupTrigger;
    public GameObject DestroyStatueTrigger;

    public GameObject MrBoulder;
    public GameObject BrokenMrBoulder;

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

    public void Start()
    {
        StartCoroutine(LateStart());
    }

    public IEnumerator LateStart()
    {
        if (EnableStartEvent)
        {
            WhiteOutCanvas canvas = FindObjectOfType<WhiteOutCanvas>();
            canvas.GetComponent<Animator>().SetTrigger("Black");
            RabbitReminderTrigger.SetActive(false);
            // Need late start to make sure everything has been initialized.
            yield return new WaitForEndOfFrame();
            canvas.GetComponent<Animator>().SetTrigger("FadeIn");
            CharacterMovement charMove = FindObjectOfType<CharacterMovement>();
            charMove.OverrideMovementVector = Vector2.right;

            StartCoroutine(RestoreCharacterControls(4f));
        }
    }

    public void Intro()
    {
        RabbitReminderTrigger.SetActive(true);
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

        BrokenThingScript thing = FindObjectOfType<BrokenThingScript>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        thing.transform.SetParent(player.transform, true);
        Animator thingAnim = thing.GetComponent<Animator>();
        thingAnim.SetTrigger("Hover");
        StartCoroutine(MoveRockToPlayerCoroutine(thing));
    }

    public IEnumerator MoveRockToPlayerCoroutine(BrokenThingScript thing)
    {
        while (thing.transform.localPosition.sqrMagnitude > 0.001)
        {
            thing.transform.localPosition = Vector3.Lerp(thing.transform.localPosition, Vector3.zero, 0.03f);
            yield return null;
        }
        yield return null;
    }

    public void DestroyStatue()
    {
        DestroyStatueTrigger.SetActive(true);

        CameraMan cameraMan = FindObjectOfType<CameraMan>();
        cameraMan.IsCinematic = true; // lol.

        BrokenThingScript thing = FindObjectOfType<BrokenThingScript>();
        thing.transform.parent = null;
        Animator thingAnim = thing.GetComponent<Animator>();
        thingAnim.SetTrigger("Shoot");

        WhiteOutCanvas canvas = FindObjectOfType<WhiteOutCanvas>();
        canvas.GetComponent<Animator>().SetTrigger("Explode");

        StartCoroutine(DestoryStatueAnimation());
        
    }

    public IEnumerator DestoryStatueAnimation()
    {
        // This is the time for the canvas to turn full white.
        yield return new WaitForSeconds(2.5f);

        // Break Mr. Boulder while the screen is white
        MrBoulder.SetActive(false);
        BrokenMrBoulder.SetActive(true);

        // Let the visuals settle before undoing Cinema mode.
        yield return new WaitForSeconds(5f);
        CameraMan cameraMan = FindObjectOfType<CameraMan>();
        cameraMan.IsCinematic = false;
    }

    public void Ending()
    {
        CameraMan cameraMan = FindObjectOfType<CameraMan>();
        cameraMan.IsCinematic = true; // lol.

        WhiteOutCanvas canvas = FindObjectOfType<WhiteOutCanvas>();
        canvas.GetComponent<Animator>().SetTrigger("FadeOut");

        CharacterMovement charMove = FindObjectOfType<CharacterMovement>();
        charMove.OverrideMovementVector = Vector2.left;
        StartCoroutine(RestoreCharacterControls(3.5f));
    }

    public IEnumerator RestoreCharacterControls(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        CharacterMovement charMove = FindObjectOfType<CharacterMovement>();
        charMove.OverrideMovementVector = Vector2.zero;
    }
}
