using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_MelleAttack : AttackEvent
{
    [SerializeField] int DMG = 1;
    [SerializeField] float cooldown = 2.5f;
    float timerCD = 2.5f;
    DamageZone damage;
    GameObject damageZone;

    void Start()
    {
        timerCD = 0;
        damage = GetComponentInChildren<DamageZone>();
        damage.damage = DMG;
        damage.targetTag = TargetTag.Enemy;
        damageZone = damage.gameObject;
        damage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (damage.damage != DMG) damage.damage = DMG;
        if (timerCD > 0) timerCD -= Time.deltaTime;
        if (SwipeManager.Instance.Tap && timerCD <= 0) Attack();
    }
    public override void Attack()
    {
        if (timerCD > 0) return;
        damageZone.SetActive(true);
        timerCD = cooldown;
    }
}
