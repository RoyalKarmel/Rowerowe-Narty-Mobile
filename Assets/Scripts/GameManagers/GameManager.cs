using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public AdsManager ads;
    public TMP_Text scoreText;
    public TMP_Text bestScoreText;
    public TMP_Text gameOverScoreText;
    public TMP_Text gameOverBestScoreText;
    public TMP_Text coinsText;
    public TMP_Text ammoText;
    public GameObject gameOverScreen;
    public GameObject uiPanel;

    public float scoreMultiplier = 1;
    private float currentScore = 0;
    private int currentBestScore = 0;
    private int coins = 0;
    private string bestScoreKey = "BestScore";
    private string coinsKey = "Coins";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

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
            SetCoinsText();
        }
    }

    void Update()
    {
        currentScore += scoreMultiplier * Time.deltaTime;
        SetScoreText();
    }

    #region Set Text
    // Set scores text
    void SetBestScoreText()
    {
        bestScoreText.text = "Best Score: <color=#f21010>" + currentBestScore.ToString() + "</color>";
    }

    void SetScoreText()
    {
        scoreText.text = "Score: <color=#f21010>" + Mathf.RoundToInt(currentScore).ToString() + "</color>";
    }

    // Set coins text
    void SetCoinsText()
    {
        coinsText.text = coins.ToString();
    }

    // Set ammo text
    public void SetAmmoText(int ammo)
    {
        ammoText.text = ammo.ToString();
    }
    #endregion

    // Update coins
    public void SetCoins()
    {
        coins++;
        PlayerPrefs.SetInt(coinsKey, coins);
        SetCoinsText();
    }

    #region Game Over
    // You lose
    public void GameOver()
    {
        Time.timeScale = 0f;
        ads.ShowBanner();
        ads.PlayInterstitialAd();

        if (currentScore > currentBestScore)
        {
            currentBestScore = Mathf.RoundToInt(currentScore);
            PlayerPrefs.SetFloat(bestScoreKey, currentBestScore);
        }

        gameOverScoreText.text = "Score: <color=#f21010>" + Mathf.RoundToInt(currentScore).ToString() + "</color>";
        gameOverBestScoreText.text = "Best Score: <color=#f21010>" + currentBestScore.ToString() + "</color>";
        gameOverScreen.SetActive(true);
        uiPanel.SetActive(false);
    }

    // Restart game
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Back to menu
    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    #endregion
}
