using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    // Enemy는 Tag에 "Enemy"로 설정됨

    // 소환 로직, 애니메이션 로직만 있으면 됨
    // 스폰 위치: x는 플레이어의 위치보다 앞 30~40f에서 미리 생성, y는 상: -1.2 중: -2.1 하: -3.4, 최상: 1.0

    // 에너미 배열 리스트 - 캐릭터의 점수에 따라 추가소환됨
    public GameObject[] enemies; // 0: 무당벌레 1: 벌 2: 톱

    // 소환은 에너미들의 콜라이더가 플레이어의 청크 체크 콜라이더에 닿으면 위치를 바꾸어 재소환
    // 별도의 스크립트에서 TriggerEnter에 접근 시 해당 스크립트의 RespawnEnemy() 함수 호출

    // 무당벌레는 -3.4에서만 스폰, 벌은 -1.2와 -2.1에서만 스폰, 톱은 전부 가능

    public void SpawnEnemy(int enemyIndex)
    {
        // 초기 소환(프리펩 생성) - 이후 Enemy 스크립트에서 TriggerEnter로 재소환
        // 무당벌레: 스코어 0부터, 벌: 스코어 300부터, 톱: 스코어 600부터

        float spawnX = GameManager.Instance.player.transform.position.x + Random.Range(20f, 30f);
        float spawnY = -3.4f; // 기본값

        if (enemyIndex < 0 || enemyIndex >= enemies.Length)
        {
            Debug.LogError("Invalid enemy index");
            return;
        }
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
        Instantiate(enemies[enemyIndex], new Vector3(spawnX, spawnY, 0), Quaternion.identity);
    }
}
