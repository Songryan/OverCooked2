using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private IPlayerState currentState;
    private FreeState freeState;
    private HoldState holdState;

    public Animator animator;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float turnSpeed = 50f;

    private Vector2 moveInput;

    private void Awake()
    {
        freeState = new FreeState(this);
        holdState = new HoldState(this);
        currentState = freeState;  // �ʱ� ���� ����
    }

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
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

    void FixedUpdate()
    {
        if(moveInput.magnitude != 0)
        {
            Vector3 moveDir = new Vector3(moveInput.x, 0f, moveInput.y);
            Vector3 movement = moveDir * moveSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);

            // transform.forward = moveDir;

            // �÷��̾ �̵��ϴ� ������ �ٶ󺸵��� ȸ��
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }

    public void CatchOrKnockback()
    {
        // �뽬 ���� ����
    }

    public void CookOrThrow()
    {
        // ������ ���� ����
        ChangeState(freeState);  // ���� ��ȯ
    }

    public void PickupOrPlace()
    {
        // �������� ���� ����
        ChangeState(freeState);  // ���� ��ȯ
    }

    public void ChangeState(IPlayerState newState)
    {
        currentState = newState;
    }

    public void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }

    public void OnDash(InputValue inputValue)
    {

    }

    public void OnCookOrThrow(InputValue inputValue)
    {

    }

    public void OnPickupOrPlace(InputValue inputValue)
    {

    }
}
