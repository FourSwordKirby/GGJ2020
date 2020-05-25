using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Letterbox : MonoBehaviour {

    public static Letterbox instance;

    public Image top;
    public Image bottom;

    public GameObject topWingPosition;
    public GameObject bottomWingPosition;

    public GameObject topPosition;
    public GameObject bottomPosition;

    public bool isOn;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (this != instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    // Update is called once per frame
    public IEnumerator TurnOn (float tweenTime = 0) {
        float counter = 0;

        while(counter <= tweenTime)
        {
            counter += Time.deltaTime;
            Debug.Log(counter);
            top.transform.position = Vector3.Lerp(topWingPosition.transform.position, topPosition.transform.position, counter / tweenTime);
            bottom.transform.position = Vector3.Lerp(bottomWingPosition.transform.position, bottomPosition.transform.position, counter / tweenTime);
            yield return new WaitForEndOfFrame();
        }

        isOn = true;
        yield return null;
	}

    public IEnumerator TurnOff(float tweenTime = 0)
    {
        float counter = 0;

        while (counter <= tweenTime)
        {
            counter += DialogueUIController.deltaTime;
            top.transform.position = Vector3.Lerp(topPosition.transform.position, topWingPosition.transform.position, counter / tweenTime);
            bottom.transform.position = Vector3.Lerp(bottomPosition.transform.position, bottomWingPosition.transform.position, counter / tweenTime);
            yield return null;
        }

        isOn = false;
        yield return null;
    }
}
