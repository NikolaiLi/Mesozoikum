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
    public AudioSource runscreamSound;
    public AudioSource footstepsSound;
    
    private bool active = false;
    public float activeTime = 5f;
    private float timer = 0;

    UnityEngine.AI.NavMeshAgent agent;
    Animator animator;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
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

    void SetChargeDirection()
    {
        Debug.Log("SetChargeDirection Activated");
        targetPoint = player.transform.position;
        Vector3 offset = (targetPoint - transform.position).normalized * 15;
        targetPoint = player.transform.position + offset;
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
        animator.SetBool("walking", true);
        animator.SetBool("charging", false);
    }

    void Chase()
    {
        if(GetDistanceToPlayer() > 40) {
            state = State.ChargeState;
            Debug.Log("Chase -> Charge");
            SetChargeDirection();
            timer = activeTime;
            return;
        }

        targetPoint = player.transform.position;
        LookAtTarget();
        agent.SetDestination(targetPoint);
        animator.SetBool("walking", true);
        animator.SetBool("charging", false);
    }

    void Charge()
    {

        timer -= Time.deltaTime;

        if(timer <= 0) 
        {
            state = State.ChaseState;
            agent.enabled = true; 
            Debug.Log("Charge -> Chase");
            runscreamSound.enabled = false;
            footstepsSound.enabled = false;

            return;
        }

        agent.enabled = false;
        Vector3 velocity = targetPoint - transform.position;
        velocity.y = 0;
        velocity.Normalize();
        velocity *= chargeSpeed * Time.deltaTime;
        LookAtTarget();
        controller.Move(velocity);
        animator.SetBool("charging", true);
        animator.SetBool("walking", false);
        runscreamSound.enabled = true;
        footstepsSound.enabled = true;

    }

    enum State
    {
        PatrolState,
        ChaseState,
        ChargeState,
    }
}