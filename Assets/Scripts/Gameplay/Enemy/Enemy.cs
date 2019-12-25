using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    [SerializeField] float timeAnimDead = 0.1f;
    [SerializeField] ParticleSystem damageParticleSystem;

    public override int HP
    {
        get => hp;
        set
        {
            if (value <= 0)
            {
                hp = 0;
                StartCoroutine(Dead());
            }
            if (value < hp)
            {
                if (damageParticleSystem != null) damageParticleSystem.Play();
                hp = value;
            }
            if (value > hp)
            {
                hp = value;
            }
        }
    }

    protected void Start()
    {
       if(damageParticleSystem != null && damageParticleSystem.isPlaying) damageParticleSystem.Stop();
        ManegerEnemy.instanse?.AddMy(this);
    }

    protected void Update()
    {
    }

    IEnumerator Dead()
    {
        ManegerEnemy.instanse?.DeleteMy(this);
        Destroy(GetComponent<Collider>());              
        foreach (Transform child in transform)          
        {
            if (child.tag == "DeadDestroy")
                Destroy(child.gameObject);
        }
        
        yield return new WaitForSeconds(timeAnimDead); 
        Destroy(this.gameObject);
    }
    
}
