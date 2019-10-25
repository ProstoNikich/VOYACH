using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Moving : Enemy
{
    //!!!!!! Нужна анимация движения, планируется поворот на 90градусов спрайта в сторону движения


    public Transform TargetMove;


    new void Start()
    {
        base.Start();
        StartCoroutine(Moving());
    }

    new void Update()
    {
        base.Update();
    }

    IEnumerator Moving()
    {
        while (true)
        {
            Move();
            yield return new WaitForSeconds(1);
        }
    }

    void Move()
    {
        //Debug.Log(" - Целимся в точку - " + TargetMove.position);
        Vector3 goThere = new Vector3(0,0,0);
        if (transform.position.x > TargetMove.position.x) goThere.x = -1;
        else if (transform.position.x < TargetMove.position.x) goThere.x = 1;

        if (transform.position.z > TargetMove.position.z) goThere.z = -1;
        else if (transform.position.z < TargetMove.position.z) goThere.z = 1;

        
        //Debug.Log(" - Идем в точку - " + goThere);
        if (CheckMove(goThere))
            transform.Translate(goThere);
    }

    bool CheckMove(Vector3 nextPosition)
    {
        Collider[] targetObjects = Physics.OverlapBox(transform.position + nextPosition, new Vector3(0.1f, 0.1f, 0.1f));

        if (targetObjects.Length == 0) return true;
        if (targetObjects.Length != 1)
            //Debug.LogError(gameObject.name + "-Ошибка!! В позиции назначения, найдено " + targetObjects.Length + " коллайдеров!!!");
            return false;
        else if (targetObjects[0].tag == "Player")
        {
            targetObjects[0].gameObject.GetComponent<Player>().Figth(this);
            return false;
        }
        return false;
    }

    protected override void Dead()
    {
        StopCoroutine(Moving());
        base.Dead();
    }
}
