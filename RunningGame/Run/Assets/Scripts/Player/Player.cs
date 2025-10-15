using UnityEngine;

// Player 오브젝트에 부착, 자동 이동만 담당
public class Player : MonoBehaviour
{
    [SerializeField] private GameObject character; // Character 오브젝트 참조
    [SerializeField] private PLAnimController animController; // Character 오브젝트의 애니메이션 컨트롤러
    [SerializeField] private float speed = 7f; // 이동 속도
    [SerializeField] private GameObject PlayerPos; // 플레이어 위치 참조 오브젝트
    private bool isMoving = false;

    private float distanceTraveled = 0f; // 누적 이동 거리
    private int lastScoreDistance = 0;   // 마지막으로 점수를 올린 거리(정수)
    private float speedIncreaseInterval = 100f; // 속도 증가 간격(거리)
    private float speedIncreaseAmount = 0.5f;  // 속도 증가량

    // 에너미 연속 소환 방지용
    private int lastLadybugSpawnScore = -1000;
    private int lastBeeSpawnScore = -1000;
    private int lastSawSpawnScore = -1000;
    private int ladybugSpawnInterval = 30; // 최소 점수 간격
    private int beeSpawnInterval = 50;
    private int sawSpawnInterval = 70;

    public void SetGameStart()
    {
        isMoving = true;
        animController.PlayRunAnim();
        distanceTraveled = 0f;
        lastScoreDistance = 0;
        speed = 7f;
        lastLadybugSpawnScore = -1000;
        lastBeeSpawnScore = -1000;
        lastSawSpawnScore = -1000;
    }

    private void Update()
    {
        if (isMoving)
        {
            float moveDelta = speed * Time.deltaTime;
            transform.Translate(Vector3.right * moveDelta);
            distanceTraveled += moveDelta;

            int currentDistanceInt = Mathf.FloorToInt(distanceTraveled);
            if (currentDistanceInt > lastScoreDistance)
            {
                int addScore = currentDistanceInt - lastScoreDistance;
                GameManager.Instance.AddScore(addScore);
                lastScoreDistance = currentDistanceInt;

                int score = GameManager.Instance.score;
                // 무당벌레: 항상 소환
                if (score - lastLadybugSpawnScore >= ladybugSpawnInterval)
                {
                    GameManager.Instance.enemySpawner.SpawnEnemy(0);
                    lastLadybugSpawnScore = score;
                }
                // 벌: 300점 이상부터 소환
                if (score >= 300 && score - lastBeeSpawnScore >= beeSpawnInterval)
                {
                    GameManager.Instance.enemySpawner.SpawnEnemy(1);
                    lastBeeSpawnScore = score;
                }
                // 톱: 600점 이상부터 소환
                if (score >= 600 && score - lastSawSpawnScore >= sawSpawnInterval)
                {
                    GameManager.Instance.enemySpawner.SpawnEnemy(2);
                    lastSawSpawnScore = score;
                }

                // 속도 증가: 일정 거리마다
                if (currentDistanceInt % (int)speedIncreaseInterval == 0)
                {
                    speed += speedIncreaseAmount;
                }
            }
        }
    }

    public void ResetPosition()
    {
        isMoving = false;
        animController.PlayIdleAnim();
        transform.position = PlayerPos.transform.position;
        speed = 7f; // 속도를 초기값으로 재설정
        distanceTraveled = 0f;
        lastScoreDistance = 0;
        lastLadybugSpawnScore = -1000;
        lastBeeSpawnScore = -1000;
        lastSawSpawnScore = -1000;
    }

}
