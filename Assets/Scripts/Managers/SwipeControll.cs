using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControll : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    [Tooltip(" Force - Add a continuous force to the rigidbody, using its mass.\n Acceleration - Add a continuous acceleration to the rigidbody,ignoring its mass.\n Impulse - Add an instant force impulse to the rigidbody, using its mass. \n VelocityChange -	Add an instant velocity change to the rigidbody, ignoring its mass.")]
    [SerializeField] ForceMode forceMode = ForceMode.Impulse;
    [SerializeField] float minForse = 1.0f;
    [SerializeField] float maxForse = 10.0f;
    
    [Header("Swipe sensitivity settings:")]
    [Tooltip("This parameter answers how long the swipe from the diagonal of the screen will count the minimum speed of the character.")]
    [Range(0.0f, 1.0f)][SerializeField] float minDistanceMultiplier = 0.01f;
    [Tooltip("This parameter answers how long the swipe from the screen diagonal will reach the maximum speed of the character.")]
    [Range(0.0f, 1.0f)][SerializeField] float maxDistanceMultiplier = 1.0f;



    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (SwipeManager.Instance.SwipeOn)
        {
            float m_Speed = SwipeManager.Instance.GetForseSwipe(minForse, minDistanceMultiplier, maxForse, maxDistanceMultiplier);

            float newRotateY = -transform.eulerAngles.y + SwipeManager.Instance.GetResistAngle();
            
            transform.Rotate(0, newRotateY, 0, Space.Self);

            m_Rigidbody.AddForce(transform.forward * m_Speed, forceMode);
        }
    }
}
