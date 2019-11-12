using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [Header("Settings gameplay:")]
    [SerializeField] protected int hp = 10;
    public virtual int HP
    {
        get => hp;
        set => hp = value;        
    }
}
