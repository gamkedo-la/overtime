using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TelepresenceBotMotor))]
public class TelepresenceBotAiSimple : StickySlowsMe
{
    [SerializeField] float m_maxForwardSpeed = 2f;
    [SerializeField] float m_rotationSpeed = 180f;

    private TelepresenceBotMotor m_motor;

    private bool m_moving;
    private bool m_turning;
    private int m_turnDirection;


    void Awake()
    {
        m_motor = GetComponent<TelepresenceBotMotor>();
    }


    void Start()
    {
        m_motor.m_maxForwardSpeed = m_maxForwardSpeed;
        m_motor.m_rotationSpeed = m_rotationSpeed;

        m_moving = true;
        m_turning = false;
    }


    void Update()
    {
        float vertical = m_moving ? 1f : 0f;
        float horizontal = m_turning ? m_turnDirection : 0f;

        m_motor.Move(vertical, horizontal);
    }


    void OnCollisionStay(Collision col)
    {
        if (m_turning || col.gameObject.CompareTag("Ground"))
            return;

        StopAllCoroutines();

        if (col.gameObject.CompareTag("Friendly"))
        {
            StartCoroutine(KeepGoingAndTurn());
        }
        else
            StartCoroutine(StopAndTurn());
    }


    private IEnumerator StopAndTurn()
    {
        m_moving = false;
        m_turning = true;
        m_turnDirection = (int) Mathf.Sign(Random.Range(-1f, 1f));

        yield return new WaitForSeconds(Random.Range(0.5f, 1f));

        m_moving = true;
        m_turning = false;
    }


    private IEnumerator KeepGoingAndTurn()
    {
        m_moving = true;
        m_turning = true;
        m_turnDirection = (int) Mathf.Sign(Random.Range(-1f, 1f));

        yield return new WaitForSeconds(Random.Range(0.5f, 1f));

        m_moving = true;
        m_turning = false;
    }
}
