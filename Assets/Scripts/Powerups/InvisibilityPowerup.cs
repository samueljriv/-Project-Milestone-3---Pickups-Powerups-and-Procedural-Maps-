using UnityEngine;

public class InvisibilityPowerup : Powerup
{
    public override void Apply(PowerupManager target)
    {
        Pawn targetPawn = target.GetComponent<Pawn>();
        if (targetPawn != null)
        {
            targetPawn.Invisible = true;
        }
    }

    public override void Remove(PowerupManager target)
    {
        Pawn targetPawn = target.GetComponent<Pawn>();
        if (targetPawn != null)
        {
            targetPawn.Invisible = false;
        }
    }
}
