using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private string m_MovmentInput_X;
    [SerializeField] private string m_MovmentInput_Y;
    [SerializeField]
    private bool isChopping;
    private PlayerMovement move;

    public bool IsChopping { get => isChopping; set => isChopping = value; }
    private void Awake()
    {
        move = this.gameObject.GetComponent<PlayerMovement>();
    }
    void Update()
    {
        float X_input = Input.GetAxis(m_MovmentInput_X);
        float Y_input = Input.GetAxis(m_MovmentInput_Y);
        if ((X_input != 0 || Y_input != 0) && !isChopping)
        {
            move.Move(X_input, Y_input);
        }

    }
}
