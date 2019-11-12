using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_MeleeAttack : AttackEvent
{
    [SerializeField] int DMG = 1; 
    [SerializeField] float cooldown = 2.5f;
    float cooldownTime = 2.5f;
    DamageZone damage;
    GameObject damageZone;

    void Start()
    {
        cooldownTime = cooldown;
        cooldown = 0;
        damage = GetComponentInChildren<DamageZone>();
        damage.damage = DMG;
        damage.targetTag = TargetTag.Player;
        damageZone = damage.gameObject;
    }

    void Update()
    {
        if(damage.damage != DMG) damage.damage = DMG;
        if (cooldown > 0) cooldown -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (cooldown <= 0 && other.tag == "Player") Attack();
    }
    private void OnTriggerStay(Collider other)
    {
        if (cooldown <= 0 && other.tag == "Player") Attack();
    }

    public override void Attack()
    {
        if (cooldown > 0) return;
        damageZone.SetActive(true);
        cooldown = cooldownTime;
    }
}
