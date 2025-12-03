using UnityEngine;
using UnityEngine.InputSystem;

// Character 오브젝트에 부착, 점프/숙이기 담당
public class PLJump : MonoBehaviour
{
    private Rigidbody2D rb; // 자신의 Rigidbody2D
    private PLAnimController animController; // Img의 애니메이션 컨트롤러
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference duckAction;
    [SerializeField] private LayerMask groundLayer; // 바닥 레이어
    [SerializeField] private float groundCheckDistance = 0.1f; // 바닥 체크 거리
    private Collider2D col;

    [Header("점프/하강 조정")]
    [SerializeField] private float fallMultiplier = 2.5f; // 하강 가속도 배수
    [SerializeField] private float fastFallMultiplier = 6f; // Duck 시 하강 가속도 배수
    private bool isFastFalling = false;
    private bool isJumping = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody2D>();
        // Img 하위 오브젝트에서 PLAnimController 자동 할당
        animController = GetComponentInChildren<PLAnimController>();
        col = GetComponent<Collider2D>();
        rb.freezeRotation = true;
    }

    private void Start()
    {
        rb.freezeRotation = true;
    }

    private void OnEnable()
    {
        if (jumpAction != null)
        {
            jumpAction.action.performed += OnJumpAction;
            jumpAction.action.Enable();
        }
        if (duckAction != null)
        {
            duckAction.action.performed += OnDuckPerformed;
            duckAction.action.canceled += OnDuckCanceled;
            duckAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (jumpAction != null)
        {
            jumpAction.action.performed -= OnJumpAction;
            jumpAction.action.Disable();
        }
        if (duckAction != null)
        {
            duckAction.action.performed -= OnDuckPerformed;
            duckAction.action.canceled -= OnDuckCanceled;
            duckAction.action.Disable();
        }
    }

    private void Update()
    {
        // 점프 후 공중에 있으면 하강 가속도 적용
        if (isJumping && !IsGrounded())
        {
            float multiplier = isFastFalling ? fastFallMultiplier : fallMultiplier;
            float extraGravity = Physics2D.gravity.y * (multiplier - 1);
            rb.AddForce(Vector2.up * extraGravity * rb.mass, ForceMode2D.Force);
        }
        // 착지 판정
        if (isJumping && IsGrounded())
        {
            isJumping = false;
            isFastFalling = false;
        }
    }

    private void OnJumpAction(InputAction.CallbackContext context)
    {
        OnJump();
    }

    public void OnJump()
    {
        if (IsGrounded() && !isJumping) // 바닥에 있을 때만 점프
        {
            animController?.PlayJump();
            // velocity를 직접 0으로 만들지 않고, 오직 AddForce만 사용
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    private void OnDuckPerformed(InputAction.CallbackContext context)
    {
        OnDuck(true);
    }

    private void OnDuckCanceled(InputAction.CallbackContext context)
    {
        OnDuck(false);
    }

    public void OnDuck(bool isDucking)
    {
        animController?.PlayDuck(isDucking);
        // 점프 중(공중)일 때만 빠른 하강 적용
        if (isDucking && isJumping && !IsGrounded())
        {
            isFastFalling = true;
        }
        else
        {
            isFastFalling = false;
        }
    }

    private bool IsGrounded()
    {
        if (col == null) return false;
        Vector2 origin = (Vector2)transform.position + Vector2.down * (col.bounds.extents.y + 0.01f);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, groundCheckDistance, groundLayer);
        return hit.collider != null;
    }
}
