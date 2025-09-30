using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject startBtn;

    public void StartBtn()
    {
        gameManager.StartGame();
        startBtn.SetActive(false);
    }

    public void ResetGame()
    {
        startBtn.SetActive(true);
    }
}
