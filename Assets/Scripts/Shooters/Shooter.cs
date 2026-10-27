using UnityEngine;

public abstract class Shooter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public abstract void Start();
    public abstract void Update();
    public abstract void Shoot(GameObject shellPrefab, float fireForce, float damageDone, float lifespan);
}
