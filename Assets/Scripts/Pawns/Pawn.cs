using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    // Scripts 
    public Mover mover;
    public Shooter shooter;
    public Health health;

    // Components

    // Variables
    public float moveSpeed;
    public float turnSpeed;
    public GameObject bulletPrefab;
    public float fireForce;
    public float damageDone;
    public float bulletLifespan;
    public float shotsPerSecond;
    public bool Invisible;

    // Start is called before the first frame update
    public virtual void Start()
    {
        mover = GetComponent<Mover>();
        shooter = GetComponent<Shooter>();
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    // Defined but will need actual explicit definitions in children
    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void RotateClockwise();
    public abstract void RotateCounterClockwise();
    public abstract void Shoot();
    public abstract void SneakToggle(bool toggleSneaking);
    public abstract void RotateTowards(Vector3 targetPosition);
}