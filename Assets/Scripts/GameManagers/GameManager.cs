using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AdsManager ads;
    public TextManager textManager;
    public GameObject gameOverScreen;
    public GameObject[] uiToHide;

    public float scoreMultiplier = 1;
    public int collectedCoins = 0;
    private float currentScore = 0;
    private int bestScore = 0;
    private int coins = 0;
    private string bestScoreKey = "BestScore";
    private string coinsKey = "Coins";

    void Start()
    {
        if (PlayerPrefs.HasKey(bestScoreKey))
        {
            bestScore = PlayerPrefs.GetInt(bestScoreKey);
            textManager.SetBestScoreText(bestScore);
        }

        if (PlayerPrefs.HasKey(coinsKey))
        {
            coins = PlayerPrefs.GetInt(coinsKey);
            textManager.SetCoinsText(coins);
        }
    }

    void Update()
    {
        currentScore += scoreMultiplier * Time.deltaTime;
        textManager.SetScoreText(currentScore);
    }

    // Update coins
    public void SetCoins(int collectedCoins)
    {
        coins += collectedCoins;
        PlayerPrefs.SetInt(coinsKey, coins);
        textManager.SetCoinsText(coins);
    }

    #region Game Over
    // You lose
    public void GameOver()
    {
        Time.timeScale = 0f;

        ads.ShowBanner();
        ads.PlayInterstitialAd();

        if (currentScore > bestScore)
        {
            bestScore = Mathf.RoundToInt(currentScore);
            PlayerPrefs.SetInt(bestScoreKey, bestScore);
        }

        textManager.SetGameOverText(currentScore, bestScore);
        gameOverScreen.SetActive(true);

        // Hide UI
        foreach (GameObject ui in uiToHide)
        {
            ui.SetActive(false);
        }

        // Destroy obstacles
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == LayerMask.NameToLayer("GameElements"))
            {
                Destroy(obj);
            }
        }
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
