using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextManager textManager;

    [Header("Database")]
    public DatabaseManager databaseManager;
    public UserInfoManager userInfoManager;
    public UpdateUser updateUser;

    private float currentScore = 0;
    private int bestScore = 0;
    private float scoreMultiplier = 1;
    private int coins = 0;
    private int collectedCoins = 0;
    private string bestScoreKey = "BestScore";
    private string coinsKey = "Coins";

    void Start()
    {
        if (databaseManager.GetUserExistence())
        {
            StartCoroutine(userInfoManager.GetUserScore((int userScore) =>
            {
                bestScore = userScore;
                textManager.SetBestScoreText(bestScore);
            }));

            StartCoroutine(userInfoManager.GetUserCoins((int userCoins) =>
            {
                coins = userCoins;
                textManager.SetCoinsText(coins);
            }));
        }
        else
        {
            if (PlayerPrefs.HasKey(bestScoreKey))
                bestScore = PlayerPrefs.GetInt(bestScoreKey);

            if (PlayerPrefs.HasKey(coinsKey))
                coins = PlayerPrefs.GetInt(coinsKey);

            textManager.SetBestScoreText(bestScore);
            textManager.SetCoinsText(coins);
        }

    }

    void Update()
    {
        currentScore += scoreMultiplier * Time.deltaTime;
        textManager.SetScoreText(currentScore);
    }

    #region Score
    // Get score & best score
    public float GetScore()
    {
        return currentScore;
    }

    public int GetBestScore()
    {
        return bestScore;
    }

    // Set score multiplier
    public void SetScoreMultiplier(float newScoreMultiplier)
    {
        scoreMultiplier = newScoreMultiplier;
    }

    // Get best score key
    public string GetBestScoreKey()
    {
        return bestScoreKey;
    }
    #endregion

    #region Coins
    // Update coins
    public void SetCoins(int collectedCoins)
    {
        coins += collectedCoins;
        PlayerPrefs.SetInt(coinsKey, coins);
        updateUser.UpdateUserCoins(coins);
        textManager.SetCoinsText(coins);

        SetCollectedCoins();
    }

    // Set collected coins
    void SetCollectedCoins()
    {
        collectedCoins++;
    }

    public int GetCollectedCoins()
    {
        return collectedCoins;
    }
    #endregion
}
