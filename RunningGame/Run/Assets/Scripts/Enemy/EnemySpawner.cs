using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // 0: 무당벌레 1: 벌 2: 톱
    private List<GameObject>[] enemyPools;
    private int[] poolSizes = new int[] { 4, 2, 2 }; // 무당벌레 최대 6, 벌/톱 2개씩(여유)

    private void Awake()
    {
        // 풀 초기화
        enemyPools = new List<GameObject>[enemyPrefabs.Length];
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            enemyPools[i] = new List<GameObject>();
            for (int j = 0; j < poolSizes[i]; j++)
            {
                GameObject obj = Instantiate(enemyPrefabs[i], Vector3.one * 1000, Quaternion.identity);
                obj.SetActive(false);
                enemyPools[i].Add(obj);
            }
        }
    }

    public void SpawnEnemy(int enemyIndex)
    {
        if (enemyIndex < 0 || enemyIndex >= enemyPrefabs.Length)
        {
            Debug.LogError("Invalid enemy index");
            return;
        }

        if (enemyIndex == 0) // 무당벌레: 1~3마리 동시 소환, 별도 소환 시 최소 12 이상 간격
        {
            // 사용 가능한 풀의 수만큼만 그룹 소환
            int available = 0;
            foreach (var obj in enemyPools[0])
            {
                if (!obj.activeInHierarchy) available++;
            }
            if (available == 0) return;
            int count = Mathf.Min(Random.Range(1, 4), available);
            float baseX;
            // 현재 활성화된 무당벌레들의 x좌표 수집
            List<float> activeLadybugXs = new List<float>();
            foreach (var obj in enemyPools[0])
            {
                if (obj.activeInHierarchy)
                    activeLadybugXs.Add(obj.transform.position.x);
            }
            int tryCount = 0;
            bool foundValid = false;
            do
            {
                baseX = GameManager.Instance.player.transform.position.x + Random.Range(20f, 30f);
                foundValid = true;
                for (int i = 0; i < count; i++)
                {
                    float spawnX = baseX + i * 1.2f;
                    foreach (float x in activeLadybugXs)
                    {
                        if (Mathf.Abs(spawnX - x) < 12f)
                        {
                            foundValid = false;
                            break;
                        }
                    }
                    if (!foundValid) break;
                }
                if (foundValid) break;
                tryCount++;
            } while (tryCount < 10); // 10회 시도 후 그냥 소환

            // 그룹 전체가 한 번에 소환되지 않으면 아무것도 소환하지 않음
            int spawned = 0;
            for (int i = 0; i < count; i++)
            {
                GameObject ladybug = GetPooledEnemy(0);
                if (ladybug != null)
                {
                    float spawnX = baseX + i * 1.2f;
                    float spawnY = -3.4f;
                    ladybug.transform.position = new Vector3(spawnX, spawnY, 0);
                    ladybug.SetActive(true);
                    spawned++;
                }
            }
            // 만약 그룹 전체가 소환되지 않았다면(풀 부족 등) 이번 소환을 모두 취소
            if (spawned < count)
            {
                foreach (var obj in enemyPools[0])
                {
                    if (obj.activeInHierarchy && obj.transform.position.x >= baseX && obj.transform.position.x < baseX + count * 1.2f + 0.1f)
                    {
                        obj.SetActive(false);
                    }
                }
            }
        }
        else if (enemyIndex == 1) // 벌: 1마리, x축 -방향 이동
        {
            GameObject bee = GetPooledEnemy(1);
            if (bee != null)
            {
                float spawnX = GameManager.Instance.player.transform.position.x + Random.Range(20f, 30f);
                float spawnY = Random.value < 0.5f ? -1.2f : -2.1f;
                bee.transform.position = new Vector3(spawnX, spawnY, 0);
                bee.SetActive(true);
                // 이동 방향 설정
                Enemy enemyScript = bee.GetComponent<Enemy>();
                if (enemyScript != null) enemyScript.SetMoveLeft(true);
            }
        }
        else if (enemyIndex == 2) // 톱: 1마리
        {
            GameObject saw = GetPooledEnemy(2);
            if (saw != null)
            {
                float spawnX = GameManager.Instance.player.transform.position.x + Random.Range(20f, 30f);
                float[] possibleY = { -1.2f, -2.1f, -3.4f, 1.0f };
                float spawnY = possibleY[Random.Range(0, possibleY.Length)];
                saw.transform.position = new Vector3(spawnX, spawnY, 0);
                saw.SetActive(true);
            }
        }
    }

    private GameObject GetPooledEnemy(int enemyIndex)
    {
        foreach (var obj in enemyPools[enemyIndex])
        {
            if (!obj.activeInHierarchy)
                return obj;
        }
        return null; // 풀에 여유가 없으면 null
    }

    public void ResetAllEnemies()
    {
        foreach (var pool in enemyPools)
        {
            foreach (var obj in pool)
            {
                obj.SetActive(false);
            }
        }
    }
}
