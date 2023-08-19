using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody m_body;
    [SerializeField] private float m_speed = 5f;

    void Start()
    {
        m_body = GetComponent<Rigidbody>();
    }

    public void Move(params object[] args)
    {
        float X_input = (float)args[0];
        float Y_input = (float)args[1];
        Vector3 MoveInput = new Vector3(X_input, 0, Y_input);
        Vector3 MoveDirection = MoveInput.normalized * m_speed;
        m_body.MoveRotation(Quaternion.LookRotation(MoveInput));
        transform.Translate(MoveDirection * Time.deltaTime, Space.World);

    }

}

