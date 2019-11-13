using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetTag { Player, Enemy }

public class DamageZone : MonoBehaviour
{
    [Header("Lifetime settings:")]
    [SerializeField] bool infiniteTime = false;
    [SerializeField] float activityTime = 1.0f;

    [Header("Gameplay settings:")]
    [Tooltip("This parameter is independently controlled by elements of type AttackEvent. Setting is for debugging only.")]
    public TargetTag targetTag = TargetTag.Enemy;
    [Tooltip("This parameter is independently controlled by elements of type AttackEvent. Setting is for debugging only.")]
    public int damage = 1;
    
    [Header("Force settings:")]
    [Tooltip(" Force - Add a continuous force to the rigidbody, using its mass.\n Acceleration - Add a continuous acceleration to the rigidbody,ignoring its mass.\n Impulse - Add an instant force impulse to the rigidbody, using its mass. \n VelocityChange -	Add an instant velocity change to the rigidbody, ignoring its mass.")]
    [SerializeField] ForceMode forceMode = ForceMode.Force;
    [SerializeField] float force = 500.0f;
    [Range(-1.0f, 1.0f)]
    [SerializeField] float deviationY = 0.5f;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag.ToString()))
        {  
            Rigidbody rigidbody = other.GetComponent<Rigidbody>();
            Vector3 direction =  other.transform.position - transform.position; //вектор соприкосновения с коллайдером
            
            //Debug.Log(direction);

            rigidbody.velocity = Vector3.zero;

            //имитация силы взрыва от точки коллайдера до точки соприкосновения,
            rigidbody.AddForceAtPosition((direction.normalized + Vector3.up * deviationY) * force, other.transform.position);

            //попытка реализовать способ удара "прямо"
            //TestAddForce(other.transform, rigidbody);

            other.GetComponent<Actor>().HP -= damage;
        }
    }
    private void OnEnable()
    {
       if(!infiniteTime) StartCoroutine(DisableMy());
    }

    private IEnumerator DisableMy()
    {
        yield return new WaitForSeconds(activityTime);
        this.gameObject.SetActive(false);
    }
       
    void TestAddForce(Transform target, Rigidbody t_rigidbody)
    {
        /*
         * Узнаём координату (в мировом пространстве) вхождения в коллайдер
         * Двигаем обьект вхождения со стороны найденой координаты
         */

    }
}
