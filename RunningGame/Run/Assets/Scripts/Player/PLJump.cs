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
            Debug.Log("OnEnable 호출됨");
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
            Debug.Log("OnDisable 호출됨");
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

    private void OnJumpAction(InputAction.CallbackContext context)
    {
        Debug.Log("스페이스 눌렀음");
        OnJump();
    }

    private void OnDuckPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("숙이기 입력 (performed)");
        animController?.PlayDuck(true);
    }

    private void OnDuckCanceled(InputAction.CallbackContext context)
    {
        Debug.Log("숙이기 입력 (canceled)");
        animController?.PlayDuck(false);
    }

    private bool IsGrounded()
    {
        Debug.Log("IsGrounded 호출됨");
        if (col == null) return false;
        // Collider의 아래쪽에서 바닥 레이어를 Raycast로 체크
        Vector2 origin = (Vector2)transform.position + Vector2.down * (col.bounds.extents.y + 0.01f);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, groundCheckDistance, groundLayer);
        return hit.collider != null;
    }

    public void OnJump()
    {
        if (IsGrounded()) // 바닥에 있을 때만 점프
        {
            animController?.PlayJump();
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
