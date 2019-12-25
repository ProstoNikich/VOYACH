using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public ParticleSystem test;
    public float timer = 3;
    float time;

    void Update()
    {
        if (time <= 0) { test.Play(); time = timer; }
        else time -= Time.deltaTime;
    }
}
