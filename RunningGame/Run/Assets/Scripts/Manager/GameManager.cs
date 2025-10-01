using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score; //현재 점수   
    public int highScore; //최고 점수
    [SerializeField] private Player player;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        player.SetGameStart();
    }

    public void GameOver()
    {
        //PlayerPrefs로 가장 높은 점수 저장
        PlayerPrefs.SetInt("HighScore", Mathf.Max(score, PlayerPrefs.GetInt("HighScore", 0)));

        ResetGame();
    }

    public void ResetGame()
    {
        score = 0;
        player.ResetPosition();
        UIManager.uiManager.SetRestartBtn();
        bGController.ResetPosition();
    }
}
