using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] float timeAnimTeleport = 0.10f;
    [SerializeField] TargetTag targetTag = TargetTag.Player;
    [SerializeField] Teleport teleportTarget;

    bool incoming = false;
    bool eneble = false;
    Coroutine _enebled;
    
    private void OnTriggerEnter(Collider other)
    {
        if (incoming) return;

        if (targetTag != TargetTag.All && other.CompareTag(targetTag.ToString()))
        {
            _enebled = StartCoroutine(Enebled());
        }
        else if (targetTag == TargetTag.All && other.GetComponent<Actor>() is Actor)
        {
            _enebled = StartCoroutine(Enebled());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!eneble || incoming) return;

        if (targetTag != TargetTag.All && other.CompareTag(targetTag.ToString()))
        {
            ToTeleport(other.gameObject);
        }
        else if (targetTag == TargetTag.All && other.GetComponent<Actor>() is Actor)
        {
            ToTeleport(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (targetTag != TargetTag.All && other.CompareTag(targetTag.ToString()))
        {
            Disabled();
        }
        else if (targetTag == TargetTag.All && other.GetComponent<Actor>() is Actor)
        {
            Disabled();
        }

    }

    void ToTeleport(GameObject target)
    {
        teleportTarget.incoming = true;
        target.transform.position = teleportTarget.transform.position;
    }




    IEnumerator Enebled()
    {
        yield return new WaitForSeconds(timeAnimTeleport);
        eneble = true;
    }
    void Disabled()
    {
        StopAllCoroutines();
        incoming = false;
        eneble = false;
    }

}
