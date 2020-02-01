using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementAnimator : MonoBehaviour
{
    public CharacterMovement movement;
    public Animator animator;

    public ParticleSystem dashParticleSystem;
    public float emitSpeed;
    public ParticleSystem poofParticleSystem;

    // Update is called once per frame
    void Update()
    {
        Vector3 velocityVector = movement.selfBody.velocity;
        bool isGrounded = Mathf.Abs(velocityVector.y) > 0.3f;//Temporary y-velocity check bc too lazy to implement proper ECB based isGrounded check

        float speedModifier = velocityVector.magnitude / CharacterMovement.walkVelocity;
        float bobbingModifier = speedModifier * (isGrounded ? 0 : 1);

        animator.SetFloat("SpeedModifier", speedModifier);
        animator.SetFloat("BobbingModifier", bobbingModifier);

        animator.SetFloat("xDirection", velocityVector.x);

        float zAngle = Mathf.Atan2(velocityVector.z, velocityVector.x) * Mathf.Rad2Deg;

        ParticleSystem.ShapeModule dashPartShape = dashParticleSystem.shape;
        dashPartShape.rotation = Vector3.right * 90 + Vector3.forward * (zAngle + 90);
        ParticleSystem.EmissionModule dashPartEmission = dashParticleSystem.emission;
        dashPartEmission.rateOverTimeMultiplier = bobbingModifier * emitSpeed;

        if (Controls.jumpInputDown())
        {
                print("jump");
                poofParticleSystem.Play();
        }
    }
}
