using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Actor _object = other.GetComponent<Actor>();
        if (_object is Actor) _object.HP = 0;
    }
}
