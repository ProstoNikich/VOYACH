using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_MoveAttack : AttackEvent
{
    [SerializeField] int DMG = 1;
    DamageZone damage;
    GameObject damageZone;

    Rigidbody rigidbodyParent;
    Vector3 prePosition;
    void Start()
    {
        rigidbodyParent = transform.parent.GetComponent<Rigidbody>();
        damage = GetComponentInChildren<DamageZone>();
        damage.damage = DMG;
        damage.targetTag = TargetTag.Enemy;
        damageZone = damage.gameObject;
        damage.gameObject.SetActive(false);
    }
    private void FixedUpdate()
    {
        //if(rigidbodyParent.velocity != Vector3.zero) damageZone.SetActive(true);
        //else damageZone.SetActive(false);
    }
    void Update()
    {
        if (damageZone.activeSelf && damage.damage != DMG) damage.damage = DMG;

        //Есть аналогичная реализация серез FixedUpdate (стр 24-25).
        //Но эта должна быть менее затратной и более стабильной, по этому необходимо тестирование этих методов
        if (SwipeManager.Instance.SwipeOn) Attack();
    }
    public override void Attack()
    {
        StartCoroutine(CorutinAttack());
    }
    IEnumerator CorutinAttack()
    {
        //Debug.Log("Move start");
        damageZone.SetActive(true);
        yield return new WaitUntil(() => rigidbodyParent.velocity != Vector3.zero);
        yield return new WaitUntil(() => rigidbodyParent.velocity == Vector3.zero);
        damageZone.SetActive(false);
        //Debug.Log("Move end");
    }
}
