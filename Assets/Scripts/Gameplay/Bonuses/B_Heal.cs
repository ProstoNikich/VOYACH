using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Heal : MonoBehaviour, IBonus
{
    public int addHP = 2;

    public void ActivateBonus(Player target)
    {
        target.HP += addHP;
        Debug.Log(this.gameObject.name + " - Бонус активирован!! ");
        Destroy(this.gameObject);
    }
}
