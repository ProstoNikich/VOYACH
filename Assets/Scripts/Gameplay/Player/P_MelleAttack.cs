using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_MelleAttack : MonoBehaviour
{
    [SerializeField] int DMG = 1;
    [SerializeField] float cooldown = 2.5f;
    float timerCD = 2.5f;
    DamageZone damageZone;
    GameObject damageZoneObject;

    void Start()
    {
        timerCD = 0;
        damageZone = GetComponentInChildren<DamageZone>();
        damageZone.damage = DMG;
        damageZone.targetTag = TargetTag.Enemy;
        damageZoneObject = damageZone.gameObject;
        damageZone.gameObject.SetActive(false);
    }

    void Update()
    {
        if (damageZone.damage != DMG) damageZone.damage = DMG;
        if (timerCD > 0) timerCD -= Time.deltaTime;
        if (SwipeManager.Instance.Tap && timerCD <= 0) Attack();
    }
    public void Attack()
    {
        if (timerCD > 0) return;
        damageZoneObject.SetActive(true);
        timerCD = cooldown;
    }
}
