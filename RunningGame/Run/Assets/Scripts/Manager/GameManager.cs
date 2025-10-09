using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score; //현재 점수   
    public int highScore; //최고 점수
    public Player player;
    public EnemySpawner enemySpawner;
    [SerializeField] private BGController bGController;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UIManager.uiManager.UpdateHighScore();
    }

    public void StartGame()
    {
        player.SetGameStart();
    }

    public void GameOver()
    {
        //PlayerPrefs로 가장 높은 점수 저장
        // 현재 점수가 최고 점수보다 높으면 갱신
        // PlayerPrefs가 없는 경우 HighScore는 UI가 표시되지 않음
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            UIManager.uiManager.UpdateHighScore();
        }

        ResetGame();
    }

    public void ResetGame()
    {
        score = 0;
        player.ResetPosition();
        UIManager.uiManager.SetRestartBtn();
        bGController.ResetPosition();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UIManager.uiManager.UpdateScore();
    }
}
