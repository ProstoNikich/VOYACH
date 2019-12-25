using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bullet : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    [SerializeField] float m_Speed = 1.0f;
    [SerializeField] float timeAnimDead = 0.1f;
    [SerializeField] float timeLive = 300f;
    [SerializeField] bool infinityLive = true;
    DamageZone damageZone;

    void Start()
    {
        damageZone = GetComponent<DamageZone>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        m_Rigidbody.velocity = transform.forward * m_Speed; 
    }
    private void Update()
    {
        if(timeLive<=0 && !infinityLive) StartCoroutine(Dead());
        if (!infinityLive) timeLive -= Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(damageZone.targetTag.ToString()) || other.CompareTag("Untagged"))
            StartCoroutine(Dead());
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(timeAnimDead);
        Destroy(this.gameObject);
    }

}
