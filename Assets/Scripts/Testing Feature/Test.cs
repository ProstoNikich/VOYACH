using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

 

    private void Start()
    {
        
    }

    void OnEnable()
    {
            Debug.Log("OnEnable!! TEST");

    }

    // Update is called once per frame
    void Update()
    {
        if (SwipeManager.Instance.Tap)
        {
            Debug.Log("Tap!! TEST");
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var x = other.gameObject.GetComponent<Player>().enabled = true;
        //var z = other.gameObject.AddComponent(typeof(Test)); //создание компанента на объекте входящим в тригер
        Destroy(this.gameObject);
    }
}
