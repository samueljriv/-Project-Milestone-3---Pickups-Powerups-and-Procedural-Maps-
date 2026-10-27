using UnityEngine;

public class TankShooter : Shooter
{
    public Transform firepointTransform;

    public override void Start() {}
    public override void Update() {}

    public override void Shoot(GameObject bulletPrefab, float fireForce, float damageDone, float lifespan)
    {
        if (bulletPrefab != null)
        {
            GameObject newBullet = Instantiate(bulletPrefab, firepointTransform.position, firepointTransform.rotation) as GameObject;
            DamageOnHit doh = newBullet.GetComponent<DamageOnHit>();
            Rigidbody rb = newBullet.GetComponent<Rigidbody>();
            if (doh != null) 
            {
                doh.damageDone = damageDone;
                doh.owner = GetComponent<Pawn>();
            }
            if (rb != null) 
            {
                rb.AddForce(firepointTransform.forward * fireForce);
            }
            Destroy(newBullet, lifespan);
        } 
        else
        {
            Debug.Log("No bullet passed!");
        }
    }
}
