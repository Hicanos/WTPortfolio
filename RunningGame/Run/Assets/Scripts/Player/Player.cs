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

    // 에너미 소환 1회만 체크용
    private bool enemySpawned0 = false;
    private bool enemySpawned1 = false;
    private bool enemySpawned2 = false;

    public void SetGameStart()
    {
        isMoving = true;
        animController.PlayRunAnim();
        distanceTraveled = 0f;
        lastScoreDistance = 0;
        speed = 7f;
        enemySpawned0 = false;
        enemySpawned1 = false;
        enemySpawned2 = false;
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

                // 에너미 소환 (각 타입별 1회만)
                if (GameManager.Instance.score <= 300 && !enemySpawned0)
                {
                    GameManager.Instance.enemySpawner.SpawnEnemy(0); // 무당벌레만 소환
                    enemySpawned0 = true;
                }
                else if (GameManager.Instance.score > 300 && GameManager.Instance.score <= 600 && !enemySpawned1)
                {
                    GameManager.Instance.enemySpawner.SpawnEnemy(1);
                    enemySpawned1 = true;
                }
                else if (GameManager.Instance.score > 600 && !enemySpawned2)
                {
                    GameManager.Instance.enemySpawner.SpawnEnemy(2);
                    enemySpawned2 = true;
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
    }
}
