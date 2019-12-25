using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Heal : MonoBehaviour
{
    [SerializeField] int addHP = 2;
    [SerializeField] float addTimer = 30;
    [SerializeField] ParticleSystem ParticleSystem;
    private MeshRenderer meshRenderer;
    bool active = true;

    private void Start()
    {
        if(ParticleSystem.isPlaying) ParticleSystem.Stop();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && active)
        {
            Player player = other.GetComponent<Player>();
            player.HP += addHP;
            player.Timer += addTimer;
            active = false;
            ParticleSystem.Play();
            StartCoroutine(Dead());
        }
    }
    IEnumerator Dead()
    {
        meshRenderer.enabled = false;
        yield return new WaitWhile(()=>ParticleSystem.isPlaying);
        Destroy(this.gameObject);
    }
}
