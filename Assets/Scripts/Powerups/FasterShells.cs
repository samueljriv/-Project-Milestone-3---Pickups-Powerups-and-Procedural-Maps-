using UnityEngine;

public class FasterShells : Powerup
{
    public override void Apply(PowerupManager target)
    {
        Pawn targetPawn = target.GetComponent<Pawn>();
        if (targetPawn != null)
        {
            targetPawn.shotsPerSecond = targetPawn.shotsPerSecond * 1.3f;
        }
    }

    public override void Remove(PowerupManager target)
    {
        Pawn targetPawn = target.GetComponent<Pawn>();
        if (targetPawn != null)
        {
            targetPawn.shotsPerSecond = targetPawn.shotsPerSecond / 1.3f;
        }
    }
}
