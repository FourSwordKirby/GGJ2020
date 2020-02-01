using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float velocity;
    public Rigidbody selfBody;

    // Update is called once per frame
    void Update()
    {
        Vector2 movementVector = Controls.getDirection();
        print(movementVector);
        selfBody.velocity = (Vector3.right * movementVector.x + Vector3.forward * movementVector.y) * velocity;
    }
}
