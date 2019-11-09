using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Gameplay settings:")]
    [SerializeField] int hp = 10;
    [SerializeField] int dmg = 5;

    [Header("Timer damage settings:")]
    [SerializeField] float timerDamage = 5.0f;
    [SerializeField] float timerActionDamage = 2.0f;
    [SerializeField] int damagePoision = 1;
    [SerializeField] float cooldownPoisionDamage = 1;

    bool activetedActionTimer = false;

    public int HP { get => hp;  }
    public int DMG { get => dmg; }
    private static Coroutine poisionCoroutine = null;

    void Start()
    {

    }

    void FixedUpdate()
    {
        
    }

    private void Update()
    {
        if (hp <= 0) StartCoroutine(Dead(0.1f));
        CheckTimer();        
        CheckAnim();

    }

    /// <summary>
    /// Данный метод служит "заглушкой" отсутствующей анимации. В дальнейм параметр bool activetedActionTimer 
    /// должен быть изменён на булевский параметр у инматора отвучающий за анимацию. А этот метод должен быть удалён
    /// </summary>
    private void CheckAnim()
    {
        if (activetedActionTimer) Debug.LogWarning("Скоро получишь урон");                        //включаем анимацию
    }

    void CheckTimer()
    {
        timerDamage -= Time.deltaTime;
        if (timerDamage <= timerActionDamage) activetedActionTimer = true;
        else activetedActionTimer = false;
        if (poisionCoroutine == null && timerDamage <= 0.0f ) 
            poisionCoroutine = StartCoroutine(PosionDamage());
        else if (poisionCoroutine != null && timerDamage > 0) StopPoisionCoroutine();
    }

    private IEnumerator PosionDamage()
    {
        Debug.LogWarning("Урон от нехватки колы раз в " + cooldownPoisionDamage + " секунд!");   //включаем анимацию
        while (hp > 0)
        {
            DamageMy(damagePoision);
            Debug.LogError("Урон от нехватки колы - " + damagePoision);
            yield return new WaitForSeconds(cooldownPoisionDamage);
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
        //...                                           //запускаем анимацию
        yield return new WaitForSeconds(timeDeadAnim);  //ждем
        Time.timeScale = 0;                             //Ставим игру на паузу
        //...                                           //Запускаем меню "смерти"
        //Destroy(this);                                  //Уничтожаем этот модуль управления персонажем
    }

   
    public void Figth(Enemy enemy)
    {
        hp -= enemy.DMG;
        enemy.DamageMy(dmg);
        //Запускаем анимацию получения урона если получили урон
    }
    public void DamageMy(int damage)
    {
        hp -= damage;
        //Запускаем анимацию получения урона если получили урон
    }

}