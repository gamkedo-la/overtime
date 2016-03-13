using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class TelepresenceBotTestControl : MonoBehaviour
{
    [SerializeField] float m_forwardSpeed = 1f;
    [SerializeField] float m_rotationSpeed = 90f;

    private Rigidbody m_rigidbody;


    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }


	void Update()
    { 
        transform.Rotate(Vector3.up * Time.deltaTime * m_rotationSpeed * Input.GetAxis("Horizontal"));
    }


    void FixedUpdate()
    {
        float verticalVelocity = m_rigidbody.velocity.y;
        float forwardVelocity = m_forwardSpeed * Input.GetAxis("Vertical");
        var newVelocity = transform.forward * forwardVelocity + Vector3.up * verticalVelocity;
        m_rigidbody.velocity = newVelocity;
    }
}
