using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Non-abstract classes can't contain abstract methods.
public abstract class Mover : MonoBehaviour
{
    public abstract void Start();
    public abstract void Move(Vector3 direction, float speed);
    public abstract void Rotate(float rotationSpeed);
}
