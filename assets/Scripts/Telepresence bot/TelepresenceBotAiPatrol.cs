using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TelepresenceBotMotor))]
public class TelepresenceBotAiPatrol : MonoBehaviour
{
    [SerializeField] float m_maxForwardSpeed = 2f;
    [SerializeField] float m_rotationSpeed = 180f;
    [SerializeField] float m_decisionRate = 0.1f;
    [SerializeField] LayerMask m_avoidanceLayerMask;
    [SerializeField] float m_rayCastHeight = 0.5f;
    [SerializeField] float m_rayCastRange = 1.5f;
    [SerializeField] float m_fallBackTurnCooldown = 1f;

    private TelepresenceBotMotor m_motor;

    private bool m_moving;
    private bool m_turning;
    private int m_turnDirection;
    private float m_fallbackTurnTime;


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

        m_fallbackTurnTime += Time.deltaTime;  
    }


    private IEnumerator CheckSurroundings()
    {
        yield return new WaitForSeconds(Random.Range(0f, m_decisionRate));

        while (true)
        {
            RaycastHit hitForward;
            RaycastHit hitForwardLeft;
            RaycastHit hitForwardRight;

            var origin = transform.position + Vector3.up * m_rayCastHeight;

            var rayDirectionForward = transform.forward * m_rayCastRange;
            var rayDirectionForwardLeft = (transform.forward - transform.right).normalized * m_rayCastRange;
            var rayDirectionForwardRight = (transform.forward + transform.right).normalized * m_rayCastRange;

            var rayForward = new Ray(origin, rayDirectionForward);
            var rayForwardLeft = new Ray(origin, rayDirectionForwardLeft);
            var rayForwardRight = new Ray(origin, rayDirectionForwardRight);

            bool impactForward;
            bool impactForwardLeft;
            bool impactForwardRight;

            if (Physics.Raycast(rayForward, out hitForward, m_rayCastRange, m_avoidanceLayerMask))
            {
                impactForward = true;
                Debug.DrawLine(origin, origin + rayDirectionForward, Color.red);
            }
            else
            {
                impactForward = false;
                Debug.DrawLine(origin, origin + rayDirectionForward, Color.green);
            }

            if (Physics.Raycast(rayForwardLeft, out hitForwardLeft, m_rayCastRange, m_avoidanceLayerMask))
            {
                impactForwardLeft = true;
                Debug.DrawLine(origin, origin + rayDirectionForwardLeft, Color.red);
            }
            else
            {
                impactForwardLeft = false;
                Debug.DrawLine(origin, origin + rayDirectionForwardLeft, Color.green);
            }

            if (Physics.Raycast(rayForwardRight, out hitForwardRight, m_rayCastRange, m_avoidanceLayerMask))
            {
                impactForwardRight = true;
                Debug.DrawLine(origin, origin + rayDirectionForwardRight, Color.red);
            }
            else
            {
                impactForwardRight = false;
                Debug.DrawLine(origin, origin + rayDirectionForwardRight, Color.green);
            }

            if (impactForwardLeft && !impactForwardRight)
            {
                m_turnDirection = 1;
                m_turning = true;
            }
            else if (impactForwardRight && !impactForwardLeft)
            {
                m_turnDirection = -1;
                m_turning = true;
            }
            else if (impactForward || (impactForwardLeft && impactForwardRight))
            {
                if (!m_turning)
                    m_turnDirection = (int) Mathf.Sign(Random.Range(-1f, 1f));

                m_turning = true;
            }
            else
            {
                m_turnDirection = 0;
                m_turning = false;
            }

            yield return new WaitForSeconds(m_decisionRate);
        }
    }


    void OnCollisionStay(Collision col)
    {
        if (m_turning || col.gameObject.CompareTag("Ground") 
            || m_fallbackTurnTime < m_fallBackTurnCooldown)
            return;

        StopAllCoroutines();

        StartCoroutine(StopAndTurn());
    }


    private IEnumerator KeepGoingAndTurn()
    {
        m_moving = true;
        m_turning = true;

        yield return new WaitForSeconds(m_decisionRate);

        m_moving = true;
        m_turning = false;
    }


    private IEnumerator StopAndTurn()
    {
        m_moving = false;
        m_turning = true;
        m_turnDirection = (int) Mathf.Sign(Random.Range(-1f, 1f));

        yield return new WaitForSeconds(Random.Range(0.5f, 1f));

        m_moving = true;
        m_turning = false;
        m_fallbackTurnTime = 0;

        StartCoroutine(CheckSurroundings());
    }
}
