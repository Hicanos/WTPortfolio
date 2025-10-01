using UnityEngine;

public class PLAnimController : MonoBehaviour
{
    [SerializeField] Animator animator; // 애니메이터 컴포넌트

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    public void PlayIdleAnim()
    {
        animator.SetBool("Run", false);
        animator.SetBool("Duck", false);
    }

    public void PlayRunAnim()
    {
        animator.SetBool("Run", true);
    }

    public void PlayHit()
    {
        animator.SetTrigger("Hit");
    }

    public void StopRun()
    {
        animator.SetBool("Run", false);
    }

    public void PlayJump()
    {
        animator.SetTrigger("Jump");
    }

    // 불리언 파라미터로 숙이기 애니메이션 제어
    public void PlayDuck(bool isDucking)
    {
        animator.SetBool("Duck", isDucking);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //부딪힌 것이 장애물인 경우, HP 감소
        //3회 부딪히면 게임 오버

        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Hp감소");
            // HP가 0이면 GameManager에서 게임오버 호출
        }
    }
}
