using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private GameManager gameManager;

    [SerializeField] private InputActionReference ExitAction; // 게임 종료 버튼 활성화(Esc)
    [SerializeField] private GameObject startBtn;
    [SerializeField] private GameObject resetBtn;
    [SerializeField] private GameObject exitBtn;
    [SerializeField] private GameObject resumeBtn;
    [SerializeField] private GameObject highScoreObj;

    [SerializeField] private Texture2D defaultCursor; // 기본 커서 이미지

    public static UIManager uiManager;

    private void Awake()
    {
        if (uiManager == null)
        {
            uiManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // 커서 변경(게임 내에서, 기존 윈도우 커서가 아닌 게임 내 커서 이미지로 변경)
    private void Start()
    {
        // 커서 이미지를 기본 커서 이미지로 설정
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    public void SetStartBtn()
    {
        startBtn.SetActive(true);
        resetBtn.SetActive(false);
        exitBtn.SetActive(false);
        resumeBtn.SetActive(false);
    }

    public void SetRestartBtn()
    {
        resetBtn.SetActive(true);
        startBtn.SetActive(false);
        exitBtn.SetActive(false);
    }

    public void StartBtn()
    {
        gameManager.StartGame();
        startBtn.SetActive(false);
        resetBtn.SetActive(false);
    }

    public void UpdateScore()
    {
        // 점수는 000000 형식으로 표시 (60일경우 000060)
        scoreText.text = gameManager.score.ToString("D6");
    }

    public void UpdateHighScore()
    {
        if(gameManager.highScore == 0)
        {
            highScoreObj.SetActive(false);
        }
        else
        {
            highScoreObj.SetActive(true);
        }
        highScoreText.text = gameManager.highScore.ToString("D6");
    }

    public void UpdateHearts(int health)
    {
        // 하트는 3개, health가 2면 2개만 보이게
        // health가 0이 되면 게임 오버
        // hearts는 0,1,2 인덱스
        // helath는 0,1,2,3 값
        // UpdateHearts(2) -> hearts[0], hearts[1] 활성화, hearts[2] 비활성화

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
        }
    }

    // Esc를 누르면 게임 일시 정지 및 재개, 종료&재시작버튼 활성화&비활성화
    
    private void OnEnable()
    {
        ExitAction.action.Enable();
        ExitAction.action.performed += OnExit;
    }
    private void OnDisable()
    {
        ExitAction.action.performed -= OnExit;
        ExitAction.action.Disable();
    }

    private void OnExit(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 1) // 게임 진행 중
        {
            Time.timeScale = 0; // 일시 정지
            exitBtn.SetActive(true);
            resumeBtn.SetActive(true);
        }
        else // 게임 일시 정지 중
        {
            Time.timeScale = 1; // 재개
            exitBtn.SetActive(false);
            resumeBtn.SetActive(false);
        }
    }

    public void ExitBtn()
    {
        Application.Quit();
    }
    public void ResumeBtn()
    {
        Time.timeScale = 1; // 재개
        exitBtn.SetActive(false);
        resumeBtn.SetActive(false);
    }
}
