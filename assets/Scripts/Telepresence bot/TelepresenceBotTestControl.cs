using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TelepresenceBotMotor))]
public class TelepresenceBotTestControl : MonoBehaviour
{
    private TelepresenceBotMotor m_motor;
    

    void Awake()
    {
        m_motor = GetComponent<TelepresenceBotMotor>();
    }


	void Update()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        m_motor.Move(vertical, horizontal);
    }
}
