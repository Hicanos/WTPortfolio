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
}
