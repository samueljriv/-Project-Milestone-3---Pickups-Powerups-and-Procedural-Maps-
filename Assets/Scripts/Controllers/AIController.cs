// using System.Diagnostics;
using UnityEngine;

public class AIController : Controller
{
    public enum AIState { Guard, Chase, Flee, Patrol, Attack, Scan, BackToPost, Idle, Alerted };
    public AIState currentState;
    public GameObject[] waypoints;
    public float waypointStopDistance;
    public float lookAheadDistance;
    public float findEnemyDistance;
    public float hearingDistance;
    public float fieldOfView;
    public GameObject target;
    public float timeToWait;

    protected float lastStateChangeTime;
    protected float waitTime;
    public int currentWaypoint = 0;

    // Start is called before the first frame update
    public override void Start()
    {
        ChangeState(AIState.Idle);
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (pawn == null)
        {
            Destroy(this);
        }
        if (target == null)
        {
            target = GameObject.Find("Player");
        }
        else
        {
            MakeDecisions();
        }
        waitTime = waitTime - Time.deltaTime;
        base.Update();
    }

    public void Shoot()
    {
        pawn.Shoot();
    }

    protected bool IsDistanceLessThan(GameObject target, float distance)
    {
        if (Vector3.Distance(pawn.transform.position, target.transform.position) < distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void MakeDecisions()
    {
        if (target != null)
        {
            switch (currentState)
            {
                case AIState.Idle:
                    DoIdleState();
                    if (IsDistanceLessThan(target, 10) && CanSee(target))
                    {
                        ChangeState(AIState.Alerted);
                    }
                    else if (waitTime <= 0)
                    {
                        ChangeState(AIState.Patrol);
                    }
                    break;
                case AIState.Alerted:
                    if (!FacingEnemy())
                    {
                        if (!IsDistanceLessThan(target, 10))
                        {
                            waitTime = timeToWait;
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
                        waitTime = timeToWait;
                        ChangeState(AIState.Idle);
                    }
                    break;
                case AIState.Attack:
                    DoAttackState();
                    if (!IsDistanceLessThan(target, 10))
                    {
                        waitTime = timeToWait;
                        ChangeState(AIState.Idle);
                    }
                    break;
                case AIState.Patrol:
                    Patrol();
                    if (IsDistanceLessThan(target, 10) && CanSee(target))
                    {
                        ChangeState(AIState.Alerted);
                    }
                    break;
            }
        }
    }

    public virtual void ChangeState(AIState newState)
    {
        currentState = newState;
        lastStateChangeTime = Time.time;

    }

    public void FaceAt(Vector3 targetPosition)
    {
        pawn.RotateTowards(targetPosition);
    }

    public void FaceAt(Transform targetTransform)
    {
        FaceAt(targetTransform.position);
    }

    public void FaceAt(Pawn targetPawn)
    {
        FaceAt(targetPawn.transform);
    }

    public void FaceAt(GameObject targetObject)
    {
        FaceAt(targetObject.GetComponent<Pawn>());
    }

    public void Seek(Vector3 targetPosition)
    {
        if (CanMoveForward())
        {
            pawn.RotateTowards(targetPosition);
            pawn.MoveForward();
        }
        else
        {
            pawn.RotateCounterClockwise();
        }
    }

    public void Seek(Transform targetTransform)
    {
        Seek(targetTransform.position);
    }

    public void Seek(Pawn targetPawn)
    {
        Seek(targetPawn.transform);
    }

    public void Seek(GameObject targetObject)
    {
        Seek(targetObject.GetComponent<Pawn>());
    }

    protected virtual void DoChaseState()
    {
        Seek(target);
    }

    protected virtual void DoIdleState() { }


    protected virtual void DoAttackState()
    {
        Seek(target);
        Shoot();
    }

    protected virtual void DoFleeState()
    {
        MoveWithAvoidance();
    }

    public bool CanHear(GameObject target)
    {
        NoiseMaker noiseMaker = target.GetComponent<NoiseMaker>();
        if (noiseMaker == null)
        {
            return false;
        }
        if (noiseMaker.volumeDistance <= 0)
        {
            return false;
        }
        float totalDistance = noiseMaker.volumeDistance + hearingDistance;
        if (Vector3.Distance(pawn.transform.position, target.transform.position) <= totalDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanSee(GameObject target)
    {
        Vector3 agentToTargetVector = target.transform.position - transform.position;
        float angleToTarget = Vector3.Angle(agentToTargetVector, pawn.transform.forward);
        if (angleToTarget < fieldOfView)
        {

            Ray theRay = new Ray();
            theRay.origin = pawn.transform.position + pawn.transform.forward;
            theRay.direction = pawn.transform.forward;
            RaycastHit hitData = new RaycastHit();
            if (Physics.Raycast(theRay, out hitData, lookAheadDistance))
            {
                Debug.Log(hitData.collider.gameObject.name);
                if (hitData.collider.gameObject == target)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    protected void Patrol()
    {
        if (waypoints.Length > currentWaypoint)
        {
            Transform waypointTf = waypoints[currentWaypoint].GetComponent<Transform>();
            Seek(waypointTf);
            if (Vector3.Distance(pawn.transform.position, waypointTf.position) <= waypointStopDistance)
            {
                currentWaypoint++;
            }
        }
        else
        {
            RestartPatrol();
        }
    }

    protected void RestartPatrol()
    {
        currentWaypoint = 0;
    }

    public void TargetPlayerOne()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.players != null)
            {
                if (GameManager.instance.players.Count > 0)
                {
                    target = GameManager.instance.players[0].pawn.gameObject;
                }
            }
        }
    }

    protected bool IsHasTarget()
    {
        return (target != null);
    }

    protected void TargetNearestTank()
    {
        Pawn[] allTanks = FindObjectsOfType<Pawn>();

        Pawn closestTank = allTanks[0];
        float closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);

        foreach (Pawn tank in allTanks)
        {
            if (Vector3.Distance(pawn.transform.position, tank.transform.position) <= closestTankDistance)
            {
                closestTank = tank;
                closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);
            }
        }

        target = closestTank.gameObject;
    }

    protected bool FacingEnemy()
    {
        Ray theRay = new Ray();
        theRay.origin = pawn.transform.position + pawn.transform.forward;
        theRay.direction = pawn.transform.forward;
        RaycastHit hitData = new RaycastHit();
        if (Physics.Raycast(theRay, out hitData, findEnemyDistance))
        {
            Debug.Log(hitData.collider.gameObject.name);
            if (hitData.collider.gameObject == target)
            {
                return true;
            }
        }
        return false;
    }

    protected bool CanMoveForward()
    {
        Ray theRay = new Ray();
        theRay.origin = pawn.transform.position + pawn.transform.forward;
        theRay.direction = pawn.transform.forward;
        RaycastHit hitData = new RaycastHit();
        if (Physics.Raycast(theRay, out hitData, lookAheadDistance))
        {
            if (hitData.collider.gameObject != target)
            {
                return false;
            }
        }
        return true;
    }

    protected void MoveWithAvoidance()
    {
        if (CanMoveForward())
        {
            pawn.MoveForward();
        }
        else
        {
            pawn.RotateClockwise();
        }
    }
}