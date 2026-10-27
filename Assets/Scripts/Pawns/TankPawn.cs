using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    public float sneakSpeedModifier;
    private float timeUntilNextEvent = 0;
    private bool sneaking = false;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (timeUntilNextEvent > 0)
        {
            timeUntilNextEvent -= Time.deltaTime;
        }
        base.Update();
    }

    public override void MoveForward()
    {
        mover.Move(transform.forward, moveSpeed * ((sneaking) ? sneakSpeedModifier : 1));
    }

    public override void MoveBackward()
    {
        mover.Move(transform.forward, -moveSpeed * ((sneaking) ? sneakSpeedModifier : 1));
    }

    public override void RotateClockwise()
    {
        mover.Rotate(turnSpeed * ((sneaking) ? sneakSpeedModifier : 1));
    }

    public override void RotateCounterClockwise()
    {
        mover.Rotate(-turnSpeed * ((sneaking) ? sneakSpeedModifier : 1));
    }


    public override void Shoot()
    {
        float secondsPerShot = 1 / shotsPerSecond;
        if (timeUntilNextEvent <= 0)
        {
            timeUntilNextEvent = secondsPerShot;
            shooter.Shoot(bulletPrefab, fireForce, damageDone, bulletLifespan);
        }
    }

    public override void SneakToggle(bool toggleSneaking)
    {
        sneaking = toggleSneaking;
    }

    public override void RotateTowards(Vector3 targetPosition)
    {
        Vector3 vectorToTarget = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
}