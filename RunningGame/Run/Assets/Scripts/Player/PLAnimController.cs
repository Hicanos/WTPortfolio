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
}
