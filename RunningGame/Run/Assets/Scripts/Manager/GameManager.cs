using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score; //현재 점수   
    public int highScore; //최고 점수
    [SerializeField] private Player player;

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
        //UI 활성화, UI 버튼을 눌러야 StartGame 실행
        //현재는 바로 게임시작
        StartGame();
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
    }
}
