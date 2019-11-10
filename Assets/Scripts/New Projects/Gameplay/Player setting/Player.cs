using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Gameplay settings:")]
    [SerializeField] int hp = 10;
    [SerializeField] int dmg = 5;

    [Header("Character fade time settings:")]
    [SerializeField] float timer = 5.0f;
    [SerializeField] float eventTime = 2.0f;
    [SerializeField] int fadingDamage = 1;
    [SerializeField] float cooldownFadingDamage = 1;

    bool activetedActionTimer = false; // должен быть изменён на булевский параметр у анматора отвучающий за анимацию. А этот метод должен быть удалён

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
        while (hp > 0)
        {
            DamageMy(fadingDamage);
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

    private IEnumerator Dead(float timeDeadAnim)
    {
        //...                                           //запускаем анимацию
        yield return new WaitForSeconds(timeDeadAnim);  //ждем
        Time.timeScale = 0;                             //Ставим игру на паузу
        //...                                           //Запускаем меню "смерти"
        //Destroy(this);                                  //Уничтожаем этот модуль управления персонажем
    }
}