using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSwipe : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    [Tooltip(" Force - Add a continuous force to the rigidbody, using its mass.\n Acceleration - Add a continuous acceleration to the rigidbody,ignoring its mass.\n Impulse - Add an instant force impulse to the rigidbody, using its mass. \n VelocityChange -	Add an instant velocity change to the rigidbody, ignoring its mass.")]
    public ForceMode forceMode = ForceMode.Impulse;


    public float minForse = 1.0f;
    public float maxForse = 10.0f;

    [Range(0.0f, 1.0f)]
    public float minDistanceMultiplier = 0.01f;
    [Range(0.0f, 1.0f)]
    public float maxDistanceMultiplier = 1.0f;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (SwipeManager.Instance.SwipeOn)
        {
            float m_Speed = SwipeManager.Instance.GetForseSwipe(minForse, minDistanceMultiplier, maxForse, maxDistanceMultiplier);

            //Debug.Log("Forse = " +m_Speed);

            //Debug.LogWarning("Angle = " + SwipeManager.Instance.GetAngle());


            transform.LookAt(SwipeManager.Instance.GetRotationTargetPoint(transform.position.y));
            m_Rigidbody.AddForce(transform.forward * m_Speed, forceMode);
            //transform.Translate(Vector3.forward * m_Speed);
        }
        
    }
}
