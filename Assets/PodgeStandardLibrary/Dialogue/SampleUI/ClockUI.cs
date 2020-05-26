using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockUI : MonoBehaviour
{

    public GameObject HourHand;
    public GameObject MinuteHand;

    public Image ClockBase;

    // Update is called once per frame
    void Update()
    {
    //    //float timeElapsedRatio = GameManager.instance.currentTime / GameManager.instance.timeLimit;

    //    //int currentMinute = (int) (timeElapsedRatio * 1080); //18 playable game hours a day, 60 minutes an hour

    //    //float currentHourAngle = ((timeElapsedRatio * 1.5f) % 1);
    //    float currentMinuteAngle = ((currentMinute % 60) / 60.0f);
    //    HourHand.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.forward * (currentHourAngle * (-360) + 90)),
    //                                                            Quaternion.Euler(Vector3.forward * ((float) (currentHourAngle * (-360) - 0.25))),
    //                                                            0.01f);
    //    MinuteHand.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.forward * (currentMinuteAngle * (-360) - 90)),
    //                                                            Quaternion.Euler(Vector3.forward * ((float)(currentMinuteAngle * (-360) - 0.25))),
    //                                                            0.01f);
    }
}
