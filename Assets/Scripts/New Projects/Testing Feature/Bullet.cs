using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Снаряд летящий в перед по оси Z, наносит урон при столкновении и самоуничтожается
/// </summary>
[System.Serializable]
public class Bullet : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public float m_Speed = 1.0f;
    public int dmg = 1;
    public bool damageEnemy = false;
    public bool damagePlayer = true;

    /* Направление полёта определяется ротацией по Y, где
     * 0 - вверх
     * 90 - в право
     * 180 - назад
     * 270 (-90) - в лево 
     */

  
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        m_Rigidbody.velocity = transform.forward * m_Speed; //а не в FixedUpdate() ?
    }

    void Dead()
    {
        //Запускаем анимацию
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(damagePlayer && other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>().DamageMy(dmg);
        }
        if(damageEnemy && other.gameObject.tag == "Enemy")
        {
            other.GetComponent<Enemy>().DamageMy(dmg);
        }
        Dead();
    }
}
