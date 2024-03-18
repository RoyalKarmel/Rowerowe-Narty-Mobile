using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text bestScoreText;
    public TMP_Text gameOverScoreText;
    public TMP_Text gameOverBestScoreText;
    public TMP_Text coinsText;
    public GameObject gameOverScreen;

    private int currentScore = 0;
    private int currentBestScore = 0;
    private int coins = 0;
    private string bestScoreKey = "BestScore";
    private string coinsKey = "Coins";

    void Start()
    {
        if (PlayerPrefs.HasKey(bestScoreKey))
        {
            currentBestScore = PlayerPrefs.GetInt(bestScoreKey);
            SetBestScoreText();
        }

        if (PlayerPrefs.HasKey(coinsKey))
        {
            coins = PlayerPrefs.GetInt(coinsKey);
            coinsText.text = coins.ToString();
        }
    }

    void Update()
    {
        currentScore++;
        SetScoreText();
    }

    void SetBestScoreText()
    {
        bestScoreText.text = "Best Score: <color=#f21010>" + currentBestScore.ToString() + "</color>";
    }

    void SetScoreText()
    {
        scoreText.text = "Score: <color=#f21010>" + currentScore.ToString() + "</color>";

        if (currentScore > currentBestScore)
        {
            currentBestScore = currentScore;
            PlayerPrefs.SetInt(bestScoreKey, currentBestScore);
            SetBestScoreText();
        }
    }

    public void gameOver()
    {
        Time.timeScale = 0f;
        gameOverScoreText.text = "Score: <color=#f21010>" + currentScore.ToString() + "</color>";
        gameOverBestScoreText.text = "Best Score: <color=#f21010>" + currentBestScore.ToString() + "</color>";
        gameOverScreen.SetActive(true);
    }

    public void restartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
