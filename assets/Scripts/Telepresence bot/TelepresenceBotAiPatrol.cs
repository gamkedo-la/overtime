using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TelepresenceBotMotor))]
public class TelepresenceBotAiPatrol : MonoBehaviour
{
    [SerializeField] float m_maxForwardSpeed = 2f;
    [SerializeField] float m_rotationSpeed = 180f;
    [SerializeField] float m_decisionRate = 0.1f;
    [SerializeField] LayerMask m_avoidanceLayerMask;

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

        StartCoroutine(CheckSurroundings());
    }


    void Update()
    {
        float vertical = m_moving ? 1f : 0f;
        float horizontal = m_turning ? m_turnDirection : 0f;

        m_motor.Move(vertical, horizontal);

        
    }


    private IEnumerator CheckSurroundings()
    {
        while (true)
        {
            m_turnDirection = (int) Mathf.Sign(Random.Range(-1f, 1f));

            yield return new WaitForSeconds(m_decisionRate);
        }
    }


    private IEnumerator KeepGoingAndTurn()
    {
        m_moving = true;
        m_turning = true;

        yield return new WaitForSeconds(m_decisionRate);

        m_moving = true;
        m_turning = false;
    }
}
