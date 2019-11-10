using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Mage : Enemy
{
    [SerializeField] GameObject bulletMesh;
    [Space(2)] 
    public float fireSpeed = 2f;
    public bool findPlayer = true;

    Transform playerTransform; //реализован через Find;
    Quaternion _quaternion;

    /* Направление поворота определяется ротацией по Y, где
     * 0 - вверх
     * 90 - в право
     * 180 - назад
     * 270 (-90) - в лево 
     */

    new void Start()
    {
        base.Start();
        if(findPlayer) playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StartCoroutine(Fire());
    }

    new void Update()
    {
        base.Update();

        if (playerTransform) RotateController();
    }

    private void RotateController()
    {
        float distX = playerTransform.position.x - transform.position.x;
        float distZ = playerTransform.position.z - transform.position.z;

        if (Mathf.Abs(distX) > Mathf.Abs(distZ))
        {
            if (distX < 0) NewRotateY(-90);
            else NewRotateY(90);
        }
        else
        {
            if (distZ > 0) NewRotateY(0);
            else NewRotateY(180);
        }
    }

    private void NewRotateY(float angleY)
    {
        transform.rotation = Quaternion.Euler(0, angleY, 0);
    }

    private void FixedUpdate()
    {
        
    }

    IEnumerator Fire()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireSpeed);
            Instantiate(bulletMesh, transform.position + transform.forward, transform.rotation);
        }
    }
    protected override void Dead()
    {
        StopCoroutine(Fire());
        base.Dead();
    }
}


//Поворот не 90 градусов
//Vector3 targetDir = target.transform.position - transform.position;
//// The step size is equal to speed times frame time.
//float step = speedRotation * Time.deltaTime;
//Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
//Debug.DrawRay(transform.position, newDir, Color.red);
//        // Move our position a step closer to the target.
//        transform.rotation = Quaternion.LookRotation(newDir);