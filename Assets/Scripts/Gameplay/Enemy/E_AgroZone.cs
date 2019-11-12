using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class E_AgroZone : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Transform target;
    [SerializeField] bool moveToPlayer = true;
    void Start()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
    }

    void Update()
    {
        if (target != null && moveToPlayer)
        {
            navMeshAgent.SetDestination(target.position);
        }
        if (target != null && !moveToPlayer)
        {
            transform.parent.LookAt(target);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            target = other.GetComponent<Transform>();
    }

}
