using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Animator animator;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed = 10f;

    private Vector2 moveInput;

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y) * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);

        // �̵� �Է��� ���� �� �ִϸ������� �̵� �Ķ���͸� false�� ����
        if (moveInput.magnitude == 0)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }
    }

    public void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }
}
