using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_MoveAttack : MonoBehaviour
{
    [SerializeField] int DMG = 1;
    DamageZone damageZone;
    GameObject damageZoneObject;

    Rigidbody rigidbodyParent;
    Vector3 prePosition;
    void Start()
    {
        rigidbodyParent = transform.parent.GetComponent<Rigidbody>();
        damageZone = GetComponentInChildren<DamageZone>();
        damageZone.damage = DMG;
        damageZone.targetTag = TargetTag.Enemy;
        damageZoneObject = damageZone.gameObject;
        damageZone.gameObject.SetActive(false);
    }
    void Update()
    {
        if (damageZoneObject.activeSelf && damageZone.damage != DMG) damageZone.damage = DMG;
        if (SwipeManager.Instance != null && SwipeManager.Instance.SwipeOn) Attack();
    }
    public void Attack()
    {
        StartCoroutine(CorutinAttack());
    }
    IEnumerator CorutinAttack()
    {
        damageZoneObject.SetActive(true);
        yield return new WaitUntil(() => rigidbodyParent.velocity != Vector3.zero);
        yield return new WaitUntil(() => rigidbodyParent.velocity == Vector3.zero);
        damageZoneObject.SetActive(false);
    }
}
