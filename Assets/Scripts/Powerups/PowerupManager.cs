using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public List<Powerup> powerups;
    private List<Powerup> removedPowerupQueue;
    void Start()
    {
        powerups = new List<Powerup>();
        removedPowerupQueue = new List<Powerup>();
    }
    void Update()
    {
    }
    public void Remove(Powerup powerupToRemove)
    {
        powerupToRemove.Remove(this);
        removedPowerupQueue.Add(powerupToRemove);
    }

    public void DecrementPowerupTimers()
    {
        foreach (Powerup powerup in powerups)
        {
            powerup.duration -= Time.deltaTime;
            if (powerup.duration <= 0)
            {
                Remove(powerup);
            }
        }
    }

    public void Add(Powerup powerupToAdd)
    {
        powerupToAdd.Apply(this);
        powerups.Add(powerupToAdd);
    }

    private void ApplyRemovePowerupsQueue()
    {
        foreach (Powerup powerup in removedPowerupQueue)
        {
            powerups.Remove(powerup);
        }
        removedPowerupQueue.Clear();
    }

    private void LateUpdate()
    {
        ApplyRemovePowerupsQueue();
    }
}