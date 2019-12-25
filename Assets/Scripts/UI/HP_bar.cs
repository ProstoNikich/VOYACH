using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_bar : MonoBehaviour
{
    [SerializeField] GameObject hp1;
    [SerializeField] GameObject hp2;
    [SerializeField] GameObject hp3;

    Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (hp1 == null) hp1 =  transform.Find("hp1").gameObject;
        if (hp2 == null) hp2 = transform.Find("hp2").gameObject;
        if (hp3 == null) hp3 = transform.Find("hp3").gameObject;
    }

    void Update()
    {
        if (player.HP >= 3 && (!hp3.activeSelf || !hp1.activeSelf || !hp2.activeSelf)) FullHP();
        if (player.HP == 2) hp3.SetActive(false);
        if (player.HP == 1) { hp3.SetActive(false); hp2.SetActive(false); }
        if (player.HP == 0) { hp3.SetActive(false); hp2.SetActive(false); hp1.SetActive(false); }
    }

    private void FullHP()
    {
        hp1.SetActive(true);
        hp2.SetActive(true);
        hp3.SetActive(true);
    }
}
