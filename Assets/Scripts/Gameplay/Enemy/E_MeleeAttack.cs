using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_MeleeAttack : MonoBehaviour
{
    [SerializeField] int DMG = 1; 
    [SerializeField] float cooldown = 2.5f;
    float cooldownTime = 2.5f;
    DamageZone damageZone;
    GameObject damageZoneObject;

    void Start()
    {
        cooldownTime = cooldown;
        cooldown = 0;
        damageZone = GetComponentInChildren<DamageZone>();
        damageZone.damage = DMG;
        damageZone.targetTag = TargetTag.Player;
        damageZoneObject = damageZone.gameObject;
        damageZone.gameObject.SetActive(false);
    }

    void Update()
    {
        if(damageZone.damage != DMG) damageZone.damage = DMG;
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

    public void Attack()
    {
        if (cooldown > 0) return;
        damageZoneObject.SetActive(true);
        cooldown = cooldownTime;
    }
}
