using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_RangeAttack : AttackEvent
{
    [SerializeField] GameObject bullet;
    [SerializeField] float cooldownAttack = 2.5f;
    [SerializeField] bool attackWithAgro = false;
    float timer = 0f;

    E_AgroZone agroZone;

     void Start()
    {
        agroZone = transform.parent.GetComponentInChildren<E_AgroZone>();
    }

    void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (attackWithAgro && agroZone != null && agroZone.IsAgred) Attack();
            else if (attackWithAgro && (agroZone == null || !agroZone.enabled))
                Debug.LogError("У объекта " + transform.parent.name + " отсутствует модуль AgroZone");
            else if (!attackWithAgro) Attack();
        }
    }

    public override void Attack()
    {
        Instantiate(bullet, transform.position, transform.rotation);
        timer = cooldownAttack;
    }
}
