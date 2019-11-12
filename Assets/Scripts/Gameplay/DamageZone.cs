using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetTag { Player, Enemy }

public class DamageZone : MonoBehaviour
{
    [SerializeField] bool infiniteTime = false;
    [SerializeField] float activityTime = 1.0f;
    [HideInInspector]public int damage = 1;
    [HideInInspector]public TargetTag targetTag = TargetTag.Enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == targetTag.ToString())
            other.GetComponent<Actor>().HP -= damage;
    }
    private void OnEnable()
    {
       if(!infiniteTime) StartCoroutine(DisableMy());
    }

    private IEnumerator DisableMy()
    {
        yield return new WaitForSeconds(activityTime);
        this.gameObject.SetActive(false);
    }
}
