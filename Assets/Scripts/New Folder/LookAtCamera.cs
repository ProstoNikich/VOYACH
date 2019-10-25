using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    GameObject camera;
    public float speedRotation = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        //transform.LookAt(camera.transform);
        Vector3 targetDir = camera.transform.position - transform.position;  // The step size is equal to speed times frame time.
        float step = speedRotation * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);   // Move our position a step closer to the target.
        transform.rotation = Quaternion.LookRotation(newDir);
        Debug.DrawRay(transform.position, newDir, Color.red);           //11111111

    }
}
