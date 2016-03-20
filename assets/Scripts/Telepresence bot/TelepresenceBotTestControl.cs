using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TelepresenceBotMotor))]
public class TelepresenceBotTestControl : MonoBehaviour
{
    [SerializeField] float m_screenHeightAdjustSpeed = 1f;

    private TelepresenceBotMotor m_motor;
    private float m_screenHeight;
    

    void Awake()
    {
        m_motor = GetComponent<TelepresenceBotMotor>();
    }


	void Update()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        m_motor.Move(vertical, horizontal);

        if (Input.GetKey(KeyCode.M))
            m_screenHeight += Time.deltaTime * m_screenHeightAdjustSpeed;

        if (Input.GetKey(KeyCode.N))
            m_screenHeight -= Time.deltaTime * m_screenHeightAdjustSpeed;

        m_screenHeight = Mathf.Clamp(m_screenHeight, -1f, 1f);

        m_motor.ScreenHeight(m_screenHeight);
    }
}
