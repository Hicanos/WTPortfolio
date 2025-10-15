using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int enemyIndex; // 0: 무당벌레 1: 벌 2: 톱
    public int EnemyIndex => enemyIndex;

    private bool moveLeft = false;
    [SerializeField] private float moveSpeed = 5f;

    public void SetMoveLeft(bool value) { moveLeft = value; }

    private void Update()
    {
        if (!gameObject.activeInHierarchy) return;
        if (enemyIndex == 1 && moveLeft) // 벌만 x축 -방향 이동
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        // 필요시 다른 타입 이동 로직 추가
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false); // BG와 충돌 시 비활성화(풀로 반환)
        }
        // Player와 충돌 시 아무것도 하지 않음 (소환/재소환은 Player.cs에서만 관리)
    }
}
