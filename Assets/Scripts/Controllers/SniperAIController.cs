using UnityEngine;

public class SniperAIController : AIController
{
    public override void MakeDecisions()
    {
        if (target != null)
        {
            switch (currentState)
            {
                case AIState.Idle:
                    DoIdleState();
                    if (IsDistanceLessThan(target, 40) && CanSee(target))
                    {
                        ChangeState(AIState.Alerted);
                    }
                    break;
                case AIState.Alerted:
                    if (!FacingEnemy())
                    {
                        if (!IsDistanceLessThan(target, 40))
                        {
                            ChangeState(AIState.Idle);
                        }
                        else
                        {
                            FaceAt(target);
                        }
                    }
                    else
                    {
                        ChangeState(AIState.Chase);
                    }
                    break;
                case AIState.Chase:
                    DoChaseState();
                    if (IsDistanceLessThan(target, 5) && CanSee(target))
                    {
                        ChangeState(AIState.Attack);
                    }
                    if (!IsDistanceLessThan(target, 10))
                    {
                        ChangeState(AIState.Idle);
                    }
                    break;
                case AIState.Attack:
                    DoAttackState();
                    if (IsDistanceLessThan(target, 10))
                    {
                        ChangeState(AIState.Flee);
                    }
                    else if (!IsDistanceLessThan(target, 40))
                    {
                        ChangeState(AIState.Idle);
                    } 
                    break;
                case AIState.Flee:
                    DoFleeState();
                    if (!IsDistanceLessThan(target, 50))
                    {
                        ChangeState(AIState.Idle);
                    }
                    break;
            }
        }
    }
}
