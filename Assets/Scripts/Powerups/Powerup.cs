using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    public float duration;
    public bool isPermanent;

    public abstract void Apply(PowerupManager target);
    public abstract void Remove(PowerupManager target);

    public void OnTriggerEnter(Collider other)
    {
        PowerupManager powerupManager = other.GetComponent<PowerupManager>();
        if (powerupManager != null)
        {
            powerupManager.Add(this);
            Destroy(gameObject);
        }
    }
}