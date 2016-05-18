using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TelepresenceBotMotor))]
public class TelepresenceBotPlayerControl : StickySlowsMe
{
    [SerializeField] float m_maxForwardSpeed = 4f;
    [SerializeField] float m_rotationSpeed = 180f;
    [SerializeField] float m_screenHeightAdjustSpeed = 1f;

	[SerializeField] TelepresenceControlChanger controlChanger;
	[SerializeField] Camera playerTakeoverCamera;



    private TelepresenceBotMotor m_motor;
    private float m_screenHeight;
    

    void Awake()
    {
        m_motor = GetComponent<TelepresenceBotMotor>();
    }


    void Start()
    {
        m_motor.m_maxForwardSpeed = m_maxForwardSpeed;
        m_motor.m_rotationSpeed = m_rotationSpeed;
    }

	void OnEnable()
	{
		playerTakeoverCamera.enabled = true;
	}

	void OnDisable()
	{
		playerTakeoverCamera.enabled = false;
		controlChanger.RestorePlayerCharacterControl ();
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

		if (Input.GetButton ("UnUse")) 
		{
			playerTakeoverCamera.enabled = false;
			controlChanger.RestorePlayerCharacterControl ();
		}
    }


}
