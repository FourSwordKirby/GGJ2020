using UnityEngine;
using System.Collections;

public class CollisionDetector2D : MonoBehaviour {

    public bool CollisionDetected;

    void OnCollisionEnter2D(Collision2D col)
    {
         CollisionDetected = true;
    }

    void OnCollisionStay2D(Collision2D col)
    {
        CollisionDetected = true;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        CollisionDetected = false;
    }
}
