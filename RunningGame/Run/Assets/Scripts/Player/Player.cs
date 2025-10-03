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
    private float speedIncreaseInterval = 10f; // 속도 증가 간격(거리)
    private float speedIncreaseAmount = 0.5f;  // 속도 증가량

    public void SetGameStart()
    {
        isMoving = true;
        animController.PlayRunAnim();
        distanceTraveled = 0f;
        lastScoreDistance = 0;
        speed = 7f;
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
