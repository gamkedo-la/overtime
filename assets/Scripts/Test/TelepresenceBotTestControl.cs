using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class TelepresenceBotTestControl : MonoBehaviour
{
    [SerializeField] float m_forwardForce = 10f;
    [SerializeField] float m_maxForwardSpeed = 2f;
    [SerializeField] float m_rotationSpeed = 90f;

    private Rigidbody m_rigidbody;
    private Animator m_anim;
    private bool m_movingBefore;
    private float m_forwardVelocity;

    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();
    }


	void Update()
    { 
        transform.Rotate(Vector3.up * Time.deltaTime * m_rotationSpeed * Input.GetAxis("Horizontal"));

        bool moving = Mathf.Abs(m_forwardVelocity) > 0.01f;

        if (moving != m_movingBefore)
        {
            m_movingBefore = moving;

            if (moving)
                m_anim.SetTrigger("Up");
            else
                m_anim.SetTrigger("Down");
        }
    }


    void FixedUpdate()
    {
        float input = Input.GetAxisRaw("Vertical");

        m_rigidbody.AddForce(transform.forward * m_forwardForce * input);

        float verticalVelocity = m_rigidbody.velocity.y;
        m_forwardVelocity = transform.InverseTransformDirection(m_rigidbody.velocity).z;

        if (Mathf.Abs(m_forwardVelocity) > m_maxForwardSpeed)
            m_forwardVelocity = Mathf.Sign(m_forwardVelocity) * m_maxForwardSpeed;

        print(m_forwardVelocity);

        var newVelocity = transform.forward * m_forwardVelocity + Vector3.up * verticalVelocity;
        m_rigidbody.velocity = newVelocity;        
    }
}
