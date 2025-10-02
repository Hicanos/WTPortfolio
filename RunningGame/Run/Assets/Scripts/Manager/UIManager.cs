using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject startBtn;
    [SerializeField] private GameObject resetBtn;

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

    public void SetStartBtn()
    {
        startBtn.SetActive(true);
        resetBtn.SetActive(false);
    }

    public void SetRestartBtn()
    {
        resetBtn.SetActive(true);
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
}
