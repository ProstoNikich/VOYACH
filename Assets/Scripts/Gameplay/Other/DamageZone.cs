using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetTag { Player, Enemy, All }

public class DamageZone : MonoBehaviour
{
    [Header("Lifetime settings:")]
    [SerializeField] float activityTime = 1.0f;
    [SerializeField] bool infiniteTime = false;

    [Header("Gameplay settings:")]
    [Tooltip("This parameter is independently controlled by elements of type AttackEvent. Setting is for debugging only.")]
    public TargetTag targetTag = TargetTag.Enemy;
    [Tooltip("This parameter is independently controlled by elements of type AttackEvent. Setting is for debugging only.")]
    public int damage = 1;
    [SerializeField] bool damageOnStay = false;
    [SerializeField] float cooldown = 2.0f;
    float timerCooldown = 0;

    [Header("Force settings:")]
    [Tooltip(" Force - Add a continuous force to the rigidbody, using its mass.\n Acceleration - Add a continuous acceleration to the rigidbody,ignoring its mass.\n Impulse - Add an instant force impulse to the rigidbody, using its mass. \n VelocityChange -	Add an instant velocity change to the rigidbody, ignoring its mass.")]
    [SerializeField] ForceMode forceMode = ForceMode.Force;
    [SerializeField] float force = 500.0f;
    [Range(-1.0f, 1.0f)]
    [SerializeField] float deviationY = 0.5f;

    private void Update()
    {
        if (timerCooldown > 0) timerCooldown -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (targetTag != TargetTag.All && other.CompareTag(targetTag.ToString()))
        {
            ToDamage(other);
        }
        else if (targetTag == TargetTag.All && other.GetComponent<Actor>() is Actor)
        {
            ToDamage(other);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (damageOnStay && timerCooldown <= 0)
        {
            if (targetTag != TargetTag.All && other.CompareTag(targetTag.ToString()))
            {
                ToDamage(other);
            }
            else if (targetTag == TargetTag.All && other.GetComponent<Actor>() is Actor)
            {
                ToDamage(other);
            }
        }
    }


    private void OnEnable()
    {
       if(!infiniteTime) StartCoroutine(DisableMy());
    }

    private void ToDamage(Collider other)
    {
        if (damageOnStay) timerCooldown = cooldown;
        Rigidbody rigidbody = other.GetComponent<Rigidbody>();
        Vector3 direction = other.transform.position - transform.position;
        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForceAtPosition((direction.normalized + Vector3.up * deviationY) * force, other.transform.position);
        other.GetComponent<Actor>().HP -= damage;
    }

    private IEnumerator DisableMy()
    {
        yield return new WaitForSeconds(activityTime);
        this.gameObject.SetActive(false);
    }
       
}
