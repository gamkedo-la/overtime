using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class TelepresenceBotMotor : MonoBehaviour
{
    public float m_maxForwardSpeed = 2f;
    public float m_rotationSpeed = 180f;

    [SerializeField] float m_forwardForce = 20f;

    private Rigidbody m_rigidbody;
    private Animator m_anim;
    private bool m_movingBefore;
    private float m_forwardVelocity;
    private float m_vertical;
    private float m_horizontal;


    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();
    }


    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * m_rotationSpeed * m_horizontal);

        bool moving = Mathf.Abs(m_forwardVelocity) > 0.01f || Mathf.Abs(m_horizontal) > 0.01f;

        if (moving != m_movingBefore)
        {
            m_movingBefore = moving;

            if (moving)
                m_anim.SetTrigger("Up");
            else
                m_anim.SetTrigger("Down");
        }
    }


    public void Move(float vertical, float horizontal)
    {
        m_vertical = vertical;
        m_horizontal = horizontal;  
    }


    public void ScreenHeight(float relativeHeight)
    {
        relativeHeight = Mathf.Clamp(relativeHeight, -1f, 1f);

        m_anim.SetFloat("Screen height", relativeHeight);
    }


    void FixedUpdate()
    {
        m_rigidbody.AddForce(transform.forward * m_forwardForce * m_vertical);

        float verticalVelocity = m_rigidbody.velocity.y;
        m_forwardVelocity = transform.InverseTransformDirection(m_rigidbody.velocity).z;

        if (Mathf.Abs(m_forwardVelocity) > m_maxForwardSpeed)
            m_forwardVelocity = Mathf.Sign(m_forwardVelocity) * m_maxForwardSpeed;

        //print(m_forwardVelocity);

        var newVelocity = transform.forward * m_forwardVelocity + Vector3.up * verticalVelocity;
        m_rigidbody.velocity = newVelocity;
    }
}
