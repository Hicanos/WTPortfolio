using UnityEngine;

// 캐릭터 이동을 담당(한 방향으로만 이동, 점프 시 올라갔다가 내려옴)
public class Player : MonoBehaviour
{
    [SerializeField] PLAnimController animController; // 애니메이션 컨트롤러
    [SerializeField] Player player; // 플레이어 자신

    
    // 점수 = 이동 거리
    // 거리가 늘어날 수록 이동 속도 증가
    int speed = 7; // 이동 속도
    int jumpPower = 15; // 점프 힘
    bool isJumping = false;
    
    // RigidBody는 하위 오브젝트에 존재함
    [SerializeField] private Rigidbody2D rb;

    private void Awake()
    {
    }

    public void SetGameStart()
    {
        // 게임 시작 시, 자동으로 이동 시작, 애니메이션 재생(애니메이션 컨트롤러)
        animController.PlayRunAnim();
        InvokeRepeating("Move", 0.1f, 0.02f); // 0.1초 후에 0.02초마다 Move 함수 실행

    }

    // 단일 방향 이동로직
    private void Move() 
    {
        // 매 프레임마다 일정한 속도로 이동
        transform.Translate(speed * Time.deltaTime, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
        // 스페이스바를 누르면 점프

        if (!isJumping)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isJumping = true;
        }
    }
}
