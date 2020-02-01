using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed;

    public Animator animator;
    public Rigidbody selfBody;

    public const float walkVelocity = 4.0f;
    public const float maxTurnRotation = 30.0f; //notes for how far the model is allowed to tilt/rotate
    public const float maxTiltRotation = 10.0f;

    // Update is called once per frame
    void Update()
    {
        Vector2 movementVector = Controls.getDirection();

        float initialSpeed = selfBody.velocity.magnitude;
        float currentSpeed = movementVector.magnitude * speed;

        selfBody.velocity = (Vector3.right * movementVector.x + Vector3.forward * movementVector.y) * speed;

        animator.SetFloat("VelocityModifier", currentSpeed / walkVelocity);
        animator.SetFloat("xDirection", selfBody.velocity.x);
        if (initialSpeed < 0.3f && 0.3f <= currentSpeed)
            animator.SetTrigger("MoveStart");
        else if (currentSpeed < 0.3f && 0.3f <= initialSpeed)
            animator.SetTrigger("MoveStop");
    }
}
