using UnityEngine;
using System.Collections;

public class TelepresenceBotTestControl : MonoBehaviour
{
    [SerializeField] float m_forwardSpeed = 1f;
    [SerializeField] float m_rotationSpeed = 90f;


	void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * m_forwardSpeed * Input.GetAxis("Vertical"));
        transform.Rotate(Vector3.up * Time.deltaTime * m_rotationSpeed * Input.GetAxis("Horizontal"));
    }
}
