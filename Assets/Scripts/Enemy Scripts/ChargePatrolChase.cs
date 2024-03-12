using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargePatrolChase : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform[] points;
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float targetRadius = 0.1f;
    [SerializeField] private float visionRange = 10;
    [SerializeField] private float visionConeAngle = 30;
    [SerializeField] private float chargeSpeed = 10;
    public int Respawn;

    private int indexOfTarget;
    private Vector3 targetPoint;
    private State state = State.PatrolState;
    private CharacterController controller;
    
    private bool active = false;
    public float activeTime = 5f;
    private float timer = 0;

    UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        controller = GetComponent<CharacterController>();
        indexOfTarget = -1;
        NextTarget();
        LookAtTarget();
        agent.SetDestination(targetPoint);
    }

    float GetDistanceToPlayer()
    {
        return (player.position - transform.position).magnitude;
    }
    
    float GetAngleToPlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        return Vector3.Angle(transform.forward, directionToPlayer);
    }

    bool SightLineObstructed()
    {
        Vector3 vectorToPlayer = player.transform.position - transform.position;
        Ray ray = new Ray(transform.position, vectorToPlayer);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, vectorToPlayer.magnitude))
        {
            GameObject obj = hitInfo.collider.gameObject;
            return obj != player.gameObject;
        }
        return false;
    }

    bool HasCaughtPlayer() {
        return GetDistanceToPlayer() < 30;
    }

    bool CanSeePlayer()
    {
        if (GetDistanceToPlayer() < visionRange)
        {
            if (GetAngleToPlayer() < visionConeAngle)
            {
                if (!SightLineObstructed())
                {
                    return true;
                }
            }
        }
        return false;
    }

    void Update()
    {
        if(state == State.PatrolState)
        {
            Patrol();
        }

        else if(state == State.ChaseState)
        {
            Chase();
        }

        else if(state == State.ChargeState)
        {
            Charge();
        }
    }

    void NextTarget()
    {
        indexOfTarget = (indexOfTarget + 1) % points.Length;
        targetPoint = points[indexOfTarget].position;
        targetPoint.y = transform.position.y;
    }

    void LookAtTarget()
    {
        Vector3 lookAt = targetPoint;
        lookAt.y = transform.position.y;

        Vector3 lookDir = (lookAt - transform.position).normalized;
        transform.forward = lookDir;
    }

    void Patrol()
    {
        if (CanSeePlayer())
        {
            state = State.ChaseState;
            Debug.Log("Patrol -> Chase");
            return;
        }

        if ((targetPoint-transform.position).magnitude < agent.radius)
        {
            NextTarget();
            LookAtTarget();
        }
        agent.SetDestination(targetPoint);
    }

    void Chase()
    {
        if(GetDistanceToPlayer() > 20) {
            state = State.ChargeState;
            Debug.Log("Chase -> Charge");
            timer = activeTime;
            return;
        }

        targetPoint = player.transform.position;
        LookAtTarget();
        agent.SetDestination(targetPoint);
    }

    void Charge()
    {

        timer -= Time.deltaTime;

        if(timer <= 0) 
        {
            state = State.ChaseState;
            agent.enabled = true; 
            Debug.Log("Charge -> Chase");
            return;
        }

        agent.enabled = false;
        targetPoint = player.transform.position;
        LookAtTarget();
        Vector3 velocity = targetPoint - transform.position;
        velocity.y = 0;
        velocity.Normalize();
        velocity *= chargeSpeed * Time.deltaTime;
        controller.Move(velocity);
    }

    enum State
    {
        PatrolState,
        ChaseState,
        ChargeState,
    }
}