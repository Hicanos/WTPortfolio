using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int enemyIndex; // 0: 무당벌레 1: 벌 2: 톱

    public int EnemyIndex => enemyIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 플레이어 앞 위치로 이동 (스폰 규칙 적용)
            float spawnX = GameManager.Instance.player.transform.position.x + Random.Range(30f, 40f);
            float spawnY = -3.4f;
            if (enemyIndex == 0) // 무당벌레
            {
                spawnY = -3.4f;
            }
            else if (enemyIndex == 1) // 벌
            {
                spawnY = Random.value < 0.5f ? -1.2f : -2.1f;
            }
            else if (enemyIndex == 2) // 톱
            {
                float[] possibleY = { -1.2f, -2.1f, -3.4f, 1.0f };
                spawnY = possibleY[Random.Range(0, possibleY.Length)];
            }
            transform.position = new Vector3(spawnX, spawnY, 0);
        }
    }
}
