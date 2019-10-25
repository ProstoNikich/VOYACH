using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Testing : MonoBehaviour
{
    public GameObject targetRotation;

    Vector2 downTouch = new Vector2();

    Vector3 test;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (SwipeManager.Instance.SwipeOn)
        {
            test = SwipeManager.Instance.GetVector3Position(transform.position.y);
            //var target = Instantiate(targetRotation, SwipeManager.Instance.GetVector3Position(transform.position.y), Quaternion.identity, this.transform);
            //Debug.Log("targetRotation = " + SwipeManager.Instance.GetVector3Position(transform.position.y).ToString());
            transform.LookAt(SwipeManager.Instance.GetVector3Position(transform.position.y));
            //transform.Translate(Vector3.forward);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            transform.LookAt(test);
        }
        
    }
}
