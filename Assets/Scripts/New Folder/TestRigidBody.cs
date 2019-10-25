using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRigidBody : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    [Tooltip(" Force - Add a continuous force to the rigidbody, using its mass.\n Acceleration - Add a continuous acceleration to the rigidbody,ignoring its mass.\n Impulse - Add an instant force impulse to the rigidbody, using its mass. \n VelocityChange -	Add an instant velocity change to the rigidbody, ignoring its mass.")]    
    public ForceMode forceMode = ForceMode.Impulse;
    public float m_Speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_Rigidbody.AddForce(Vector3.forward * m_Speed, forceMode);
            //m_Rigidbody.velocity = Vector3.forward * m_Speed;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            m_Rigidbody.AddForce(-Vector3.forward * m_Speed, forceMode);
            //m_Rigidbody.velocity = -Vector3.forward * m_Speed;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            m_Rigidbody.AddForce(-Vector3.right * m_Speed, forceMode);
            //m_Rigidbody.velocity = -Vector3.right * m_Speed;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            m_Rigidbody.AddForce(Vector3.right * m_Speed, forceMode);
            //m_Rigidbody.velocity = Vector3.right * m_Speed;
        }
    }
}
