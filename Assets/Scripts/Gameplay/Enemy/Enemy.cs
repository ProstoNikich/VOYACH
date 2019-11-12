using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    [SerializeField] float timeAnimDead = 0.1f;

    public override int HP
    {
        get => hp;
        set
        {
            if (value <= 0)
            {
                hp = 0;
                StartCoroutine(Dead());
            }
            if (value < hp)
            {
                //Включаем анимацию получения урона
                hp = value;
            }
            if (value > hp)
            {
                //Включаем анимацию хила
                hp = value;
            }
        }
    }

    protected void Start()
    {
    }

    protected void Update()
    {
    }

    IEnumerator Dead()
    {
        //...                                           //запускаем анимацию
        Destroy(GetComponent<Collider>());              //Уничтожаем коллайдер, что бы не взаимодействовать с объектом

        foreach (Transform child in transform)          //Уничтожаем все прикреплённые компоненты, которым суждено умереть
        {
            if (child.tag == "DeadDestroy")
                Destroy(child.gameObject);
        }
        
        yield return new WaitForSeconds(timeAnimDead);  //ждем анимацию
        Destroy(this.gameObject);                       //уничтожаем объект
    }    
}
