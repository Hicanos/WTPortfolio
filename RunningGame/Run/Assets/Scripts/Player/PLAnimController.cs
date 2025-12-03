using UnityEngine;

public class PLAnimController : MonoBehaviour
{
    [SerializeField] Animator animator; // 애니메이터 컴포넌트

    // Player HP 관련 변수
    private int maxHealth = 3; // Maximum health
    [SerializeField] private int currentHealth; // Current health

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        currentHealth = maxHealth; // Initialize current health to maximum health
    }

    public void PlayIdleAnim()
    {
        animator.SetBool("Run", false);
        animator.SetBool("Duck", false);
        //트리거 리셋
        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Hit");
        currentHealth = maxHealth;
        UIManager.uiManager.UpdateHearts(currentHealth);
    }

    public void PlayRunAnim()
    {
        animator.SetBool("Run", true);
    }

    private void PlayHit()
    {
        animator.SetTrigger("Hit");
        SoundManager.soundManager.SfxStart(2); // 충돌 효과음 재생
    }

    public void StopRun()
    {
        animator.SetBool("Run", false);
    }

    public void PlayJump()
    {
        animator.SetTrigger("Jump");
        SoundManager.soundManager.SfxStart(1); // 점프 효과음 재생
    }

    // 불리언 파라미터로 숙이기 애니메이션 제어
    public void PlayDuck(bool isDucking)
    {
        animator.SetBool("Duck", isDucking);
        SoundManager.soundManager?.SfxStart(4); // 슬라이드 효과음 재생
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //부딪힌 것이 장애물인 경우, HP 감소
        //3회 부딪히면 게임 오버

        if (collision.CompareTag("Enemy"))
        {
            PlayHit();
            currentHealth--;
            UIManager.uiManager.UpdateHearts(currentHealth);
            Debug.Log("Hp감소");
            if (currentHealth <= 0)
            {
                Debug.Log("Game Over");
                GameManager.Instance.GameOver();
            }
        }
    }
}
