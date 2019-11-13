using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class E_AgroZone : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Transform target;
    [SerializeField] bool moveToPlayer = true;
    [SerializeField] bool clearTargetOnExit = false;
    [SerializeField] float stopDistance = 2.1f;
       
    public bool IsAgred { get; private set; }

    void Start()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        IsAgred = false;
    }

    void Update()
    {
        if (target != null && moveToPlayer)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(target.position);
            navMeshAgent.stoppingDistance = stopDistance;

            if (navMeshAgent.velocity == Vector3.zero)
            {
                CorrectRotation();
            }
        }
        else if (target != null && !moveToPlayer)
        {
            navMeshAgent.isStopped = true;
            CorrectRotation();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") { 
            target = other.GetComponent<Transform>();
            if (moveToPlayer) {
                navMeshAgent.isStopped = false;
                IsAgred = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (clearTargetOnExit) {
            target = null;
            IsAgred = false;
        }
    }
    void CorrectRotation()
    {
        Vector3 targetposition = target.position;
        targetposition.y = transform.position.y;
        Vector3 targetDir = targetposition - transform.position;

        // The step size is equal to speed times frame time.
        float step = navMeshAgent.angularSpeed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);

        // Move our position a step closer to the target.
        transform.parent.rotation = Quaternion.LookRotation(newDir);
    }
}
