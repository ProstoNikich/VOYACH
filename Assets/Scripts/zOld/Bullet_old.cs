//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[System.Serializable] //??
//public class Bullet_old : MonoBehaviour
//{
//    public bool MovingSteps = false;
//    public bool autoTarget = false;
//    public GameObject target;
//    public float speedRotation = 1.0f;
//    public float speed = 1f;



//    void Start()
//    {
//        if (autoTarget)
//            target = GameObject.FindGameObjectWithTag("Player");
//        if(MovingSteps) StartCoroutine(Moving());
//        //Debug.Log();
//    }

//    private void FixedUpdate()
//    {
//        if (autoTarget && !MovingSteps)
//        {
//            Vector3 targetDir = target.transform.position - transform.position;  // The step size is equal to speed times frame time.
//            float step = speedRotation * Time.deltaTime;
//            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);   // Move our position a step closer to the target.
//            transform.rotation = Quaternion.LookRotation(newDir);
//            Debug.DrawRay(transform.position, newDir, Color.red);           //11111111
//        }
//        if (!MovingSteps) transform.Translate(transform.forward * Time.deltaTime * speed);
//    }

//    void Update()
//    {
//    }

//    IEnumerator Moving()
//    {
//        while (true)
//        {
//            if(autoTarget)
//                transform.Translate(Move());
//            else
//                transform.Translate(transform.forward);
//            yield return new WaitForSeconds(1);
//        }
//    }
//    Vector3 Move()
//    {
//        //Debug.Log(" - Целимся в точку - " + TargetMove.position);
//        Vector3 goThere = new Vector3(0, 0, 0);
//        if (transform.position.x > target.transform.position.x) goThere.x = -1;
//        else if (transform.position.x < target.transform.position.x) goThere.x = 1;

//        if (transform.position.z > target.transform.position.z) goThere.z = -1;
//        else if (transform.position.z < target.transform.position.z) goThere.z = 1;

//        return goThere;
//    }
//}
