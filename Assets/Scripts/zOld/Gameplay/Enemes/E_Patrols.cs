//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class E_Patrols : Enemy
//{
//    [Space][Header("Setting Patrols points:")]
//    [SerializeField] bool circularMotion = true;
//    [SerializeField] float stepsCD = 1.0f;
//    [SerializeField] Vector3[] points;

//    new void Start()
//    {
//        base.Start();
//        StartCoroutine(StartMove());
//    }

//    new void Update()
//    {
//        base.Update();
//    }

//    IEnumerator StartMove()
//    {
//        int i = 0;
//        if (circularMotion)
//        {
//            while (true)
//            {
//                //Debug.Log(gameObject.name+ " - Иду в точку: " + points[i]);                
//                yield return StartCoroutine(Moving(points[i]));
//                i++;
//                if (i == points.Length) i = 0;
//            }
//        }
//        else
//        {
//            bool increase = true;
//            while (true)
//            {
//                //Debug.Log(gameObject.name+ " - Иду в точку: " + points[i]);
//                yield return StartCoroutine(Moving(points[i]));
//                if (increase) ++i;
//                else --i;
//                if (i == points.Length && increase)
//                {
//                    i -= 2;
//                    increase = false;
//                }
//                if (i == -1 && !increase)
//                {
//                    i = 1;
//                    increase = true;
//                }
//            }
//        }
//    }

//    IEnumerator Moving(Vector3 TargetMove)
//    {
//        while (transform.position != TargetMove)
//        {
//            Move(TargetMove);
//            yield return new WaitForSeconds(stepsCD);
//        }
//    }

//    void Move(Vector3 TargetMove)
//    {
//        Vector3 goThere = new Vector3(0, 0, 0);
//        if (transform.position.x != TargetMove.x)
//        {
//            if (transform.position.x > TargetMove.x) goThere.x = -1;
//            else if (transform.position.x < TargetMove.x) goThere.x = 1;

//            if (CheckMove(goThere))
//                transform.Translate(goThere);
//            return;
//        }

//        if (transform.position.z > TargetMove.z) goThere.z = -1;
//        else if (transform.position.z < TargetMove.z) goThere.z = 1;

//        if (CheckMove(goThere))
//            transform.Translate(goThere);
//    }

//    bool CheckMove(Vector3 nextPosition)
//    {
//        Collider[] targetObjects = Physics.OverlapBox(transform.position + nextPosition, new Vector3(0.1f, 0.1f, 0.1f));

//        if (targetObjects.Length == 0) return true;
//        if (targetObjects.Length != 1) { 
//            Debug.LogError(gameObject.name + "- Ошибка!! В позиции назначения, найдено " + targetObjects.Length + " коллайдеров!!!");
//            return false;
//        }
//        else if (targetObjects[0].tag == "Player")
//        {
//            targetObjects[0].gameObject.GetComponent<Player>();//.Figth(this);
//            return false;
//        }
//        return false;
//    }

//    protected override void Dead()
//    {
//        StopCoroutine(StartMove());
//        base.Dead();
//    }
//}
