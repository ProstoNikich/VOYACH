using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class E_Patrols : MonoBehaviour
{
    [Header("Settings gameplay:")]
    [SerializeField] bool activeAgroZone = true;
    [SerializeField] bool returnToPatrul = true;
    [SerializeField] bool movementY = false;

    [Header("Setting Patrols points:")]
    [SerializeField] bool circularMotion = true;
    [SerializeField] Vector3[] points = new Vector3[] { Vector3.zero, Vector3.zero, Vector3.zero};

    NavMeshAgent navMeshAgent;
    E_AgroZone agroZone;
    Coroutine coroutine;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (activeAgroZone) agroZone = GetComponentInChildren<E_AgroZone>();

        if (points.Length > 0)coroutine = StartCoroutine(StartMove());
    }

    void Update()
    {
        if (agroZone != null && agroZone.IsAgred)
        {
            StopAllCoroutines();
            coroutine = null;
        }
        else if (returnToPatrul && coroutine == null && points.Length > 0 &&  agroZone != null && !agroZone.IsAgred)
        {
            coroutine = StartCoroutine(StartMove());
        }
    }

    void test()
    {
        /*
         * 1. Двигаемся к точке
         * 2. Когда достигаем её, идем к следующе   
         * 3. Если это последняя точка
         *      3а. Если движение по кругу то идем к первой точки и начинаем двигаться по новой
         *      3б. Иначе начинаем двигаться в обратную сторону         * 
         */
    }


    IEnumerator StartMove()
    {
        navMeshAgent.stoppingDistance = 0;
        int i = 0;
        if (circularMotion)
        {
            while (true)
            {              
                yield return StartCoroutine(Moving(points[i]));
                i++;
                if (i == points.Length) i = 0;
            }
        }
        else
        {
            bool increase = true;
            while (true)
            {
                yield return StartCoroutine(Moving(points[i]));
                if (increase) ++i;
                else --i;
                if (i == points.Length && increase)
                {
                    i -= 2;
                    increase = false;
                }
                if (i == -1 && !increase)
                {
                    i = 1;
                    increase = true;
                }
            }
        }
    }
    IEnumerator Moving(Vector3 TargetMove)
    {
        navMeshAgent.SetDestination(TargetMove);

        if(movementY) yield return new WaitUntil(() => transform.position == TargetMove);
        else yield return new WaitUntil(() => transform.position.x == TargetMove.x && transform.position.z == TargetMove.z);        
     }
}
