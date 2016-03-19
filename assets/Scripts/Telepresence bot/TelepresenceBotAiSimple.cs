using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TelepresenceBotMotor))]
public class TelepresenceBotAiSimple : MonoBehaviour
{
    private TelepresenceBotMotor m_motor;

    private bool m_move;
    private bool m_turn;
    private bool m_turning;
    private int m_turnDirection;


    void Awake()
    {
        m_motor = GetComponent<TelepresenceBotMotor>();
    }


    void Start()
    {
        m_move = true;
        m_turn = false;
    }


    void Update()
    {
        float vertical = m_move ? 1f : 0f;
        float horizontal = m_turn ? m_turnDirection : 0f;

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
        m_move = false;
        m_turn = true;
        m_turning = true;
        m_turnDirection = (int) Mathf.Sign(Random.Range(-1f, 1f));

        yield return new WaitForSeconds(Random.Range(0.5f, 1f));

        m_move = true;
        m_turn = false;
        m_turning = false;
    }


    private IEnumerator KeepGoingAndTurn()
    {
        m_move = true;
        m_turn = true;
        m_turning = true;
        m_turnDirection = (int) Mathf.Sign(Random.Range(-1f, 1f));

        yield return new WaitForSeconds(Random.Range(0.5f, 1f));

        m_move = true;
        m_turn = false;
        m_turning = false;
    }
}
