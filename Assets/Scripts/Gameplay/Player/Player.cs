using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
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
                if(damageParticleSystem!=null) damageParticleSystem.Play();
                 hp = value;
            }
            if (value > hp)
            {
                if (value >= 3) hp = 3;
                else hp = value;
            }
        }
    }

    [SerializeField] float timeToDeadScreen = 2.0f;
    [SerializeField] GameObject deadPanel;
    [SerializeField] ParticleSystem damageParticleSystem;


    [Header("Character fade time settings:")]
    [SerializeField] float timer = 5.0f;
    [SerializeField] float eventTime = 2.0f;
    [SerializeField] int fadingDamage = 1;
    [SerializeField] float cooldownFadingDamage = 1;
    private Coroutine poisionCoroutine = null;
    public float Timer { get => timer; set => timer = value; }
    public float TimeToDeadScreen { get => timeToDeadScreen; }
    public float TimerTimeToDeadScreen { get; set; }

    Animator animator;
    bool activetedActionTimer = false;

    bool animMove = false;

    Rigidbody myRigidbody;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        if (damageParticleSystem != null) damageParticleSystem.Stop();
    }

    void FixedUpdate()
    {

    }

    void Update()
    {
        if (SwipeManager.Instance != null) 
            if (SwipeManager.Instance.SwipeOn && !animMove) StartCoroutine(MoveAnim());

        CheckTimer();
        CheckAnim();
    }

    private IEnumerator MoveAnim()
    {
        animMove = true;
        animator.SetBool("Move", true);
        yield return new WaitUntil(() => myRigidbody.velocity != Vector3.zero);
        yield return new WaitUntil(() => myRigidbody.velocity == Vector3.zero);
        animMove = false;
        animator.SetBool("Move", false);
    }

    private void CheckAnim()
    {
        if (activetedActionTimer) Debug.LogWarning("Скоро получишь урон");
    }

    void CheckTimer()
    {
        timer -= Time.deltaTime;
        if (timer <= eventTime) activetedActionTimer = true;
        else activetedActionTimer = false;
        if (poisionCoroutine == null && timer <= 0.0f)
            poisionCoroutine = StartCoroutine(PosionDamage());
        if (poisionCoroutine != null && timer > 0) StopPoisionCoroutine();
    }

    private IEnumerator PosionDamage()
    {
        Debug.LogWarning("Урон от нехватки колы раз в " + cooldownFadingDamage + " секунд!");
        while (HP > 0)
        {
            HP -= fadingDamage;
            Debug.LogError("Урон от нехватки колы - " + fadingDamage);
            yield return new WaitForSeconds(cooldownFadingDamage);
        }
    }
    private void StopPoisionCoroutine()
    {
        Debug.LogWarning("Таймер обновлен! \nПерестаём получать урон.");
        if (poisionCoroutine != null)
        {
            StopCoroutine(poisionCoroutine);
            poisionCoroutine = null;
        }
    }

    private IEnumerator Dead()
    {
        SwipeControll.blocked = true;
        if(animator != null) animator.SetBool("Death", true);
        yield return new WaitForSeconds(timeToDeadScreen);        
        Time.timeScale = 0;
        if (deadPanel != null) deadPanel.SetActive(true);
    }
}