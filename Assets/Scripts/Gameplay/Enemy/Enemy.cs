using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Settings gameplay:")]
    [SerializeField] int hp = 10;
    [SerializeField] int dmg = 5;
    public int HP { get => hp; }
    public int DMG { get => dmg; } //!!!

    protected void Start()
    {
    }

    protected void Update()
    {
        if (HP <= 0) Dead();
    }

    protected virtual void Dead()
    {
        //Запускаем анимацию
        Destroy(this.gameObject);
    }
    public void DamageMy(int damage)
    {
        hp -= damage;
        //Запускаем анимацию получения урона если получили урон
    }
}
