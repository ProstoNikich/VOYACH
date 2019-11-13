using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 delegate void ActiveAttacked();

public class Player : Actor
{
    public override int HP { get => hp;
        set {
            if (value <= 0) {
                hp = 0;
                StartCoroutine(Dead(0.1f));
            } 
            if( value < hp)
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

    [Header("Character fade time settings:")]
    [SerializeField] float timer = 5.0f;
    [SerializeField] float eventTime = 2.0f;
    [SerializeField] int fadingDamage = 1;
    [SerializeField] float cooldownFadingDamage = 1;
    private Coroutine poisionCoroutine = null;
    bool activetedActionTimer = false; // должен быть изменён на булевский параметр у анматора отвучающий за анимацию.

    //ActiveAttacked Attacked;
    Rigidbody myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        
    }

    void Update()
    {
        if (SwipeManager.Instance.Tap) ; //Attacked();Debug.LogWarning("Tap!!")

        //if (Hp <= 0) StartCoroutine(Dead(0.1f)); делаем это в сеттере
        CheckTimer();        
        CheckAnim();

    }

    /// <summary>
    /// Данный метод служит "заглушкой" отсутствующей анимации. В дальнейм параметр bool activetedActionTimer 
    /// должен быть изменён на булевский параметр у анматора отвучающий за анимацию. А этот метод должен быть удалён
    /// </summary>
    private void CheckAnim()
    {
        if (activetedActionTimer) Debug.LogWarning("Скоро получишь урон");                        //включаем анимацию
    }

    void CheckTimer()
    {
        timer -= Time.deltaTime;
        if (timer <= eventTime) activetedActionTimer = true;
        else activetedActionTimer = false;
        if (poisionCoroutine == null && timer <= 0.0f ) 
            poisionCoroutine = StartCoroutine(PosionDamage());
        else if (poisionCoroutine != null && timer > 0) StopPoisionCoroutine();
    }

    private IEnumerator PosionDamage()
    {
        Debug.LogWarning("Урон от нехватки колы раз в " + cooldownFadingDamage + " секунд!");   //включаем анимацию
        while (HP > 0)
        {
            HP -= fadingDamage;
            Debug.LogError("Урон от нехватки колы - " + fadingDamage);
            yield return new WaitForSeconds(cooldownFadingDamage);
        }
    }
    private void StopPoisionCoroutine()
    {
        Debug.LogWarning("Таймер обновлен! \nПерестаём получать урон.");                        //Отключаем анимацию PosionDamage()
        if (poisionCoroutine != null)
        {
            poisionCoroutine = null;
            StopCoroutine(PosionDamage());
        }            
    }
    
   private IEnumerator Dead(float timeDeadAnim)
    {
        //...                                           //блокируем управление
        //...                                           //запускаем анимацию
        yield return new WaitForSeconds(timeDeadAnim);  //ждем анимацию
        Time.timeScale = 0;                             //Ставим игру на паузу
        //...                                           //Запускаем меню "смерти"
    }
}