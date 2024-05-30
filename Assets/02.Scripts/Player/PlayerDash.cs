using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerDash : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    private Rigidbody rb;

    [Header("Dashing")]
    public float dashForce;
    public float dashDuration;
    private bool isDashing = false;

    [Header("Settings")]
    public bool allowAllDirections = true;
    public bool disableGravity = false;
    public bool resetVel = true;

    [Header("Cooldown")]
    public float dashCd;
    private float dashCdTimer;

    [Header("Dash Curve")]
    public AnimationCurve dashCurve; // AnimationCurve를 대시의 세기 변화에 사용

    [Header("Player Master Controller")]
    public PlayerMasterController2 masterController;

    [Header("Mobile Button")]
    public Button dashButton; // UI 버튼 참조

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (dashButton != null)
        {
            dashButton.onClick.AddListener(MobileDash); // 버튼 클릭 이벤트에 MobileDash 메서드 연결
        }
    }

    private void Update()
    {
        if (dashCdTimer > 0)
            dashCdTimer -= Time.deltaTime;
    }

    public void OnDash(InputValue inputButton)
    {
        if (inputButton.isPressed && !isDashing && masterController.currentPlayer == this.gameObject)
            Dash();
    }

    // UI 버튼을 클릭할 때 호출될 메서드
    private void MobileDash()
    {
        // 현재 활성화 상태인지 확인해야할듯
        if (!isDashing && dashCdTimer <= 0 && masterController.currentPlayer == this.gameObject)
            Dash();
    }

    private void Dash()
    {
        if (dashCdTimer > 0 || isDashing)
            return;

        dashCdTimer = dashCd;
        isDashing = true;

        if (disableGravity)
            rb.useGravity = false;

        if (resetVel)
            rb.velocity = Vector3.zero;

        StartCoroutine(ExecuteDash());
    }

    private IEnumerator ExecuteDash()
    {
        float elapsedTime = 0f;
        PlayerPuff.Instance.BoostPuff(transform);

        while (elapsedTime < dashDuration)
        {
            float dashProgress = elapsedTime / dashDuration;
            float curveValue = dashCurve.Evaluate(dashProgress); // AnimationCurve에서 값을 평가
            Vector3 forceToApply = transform.forward * dashForce * curveValue;
            rb.AddForce(forceToApply, ForceMode.Impulse);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isDashing = false;

        if (disableGravity)
            rb.useGravity = true;
    }
}
