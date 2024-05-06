using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public AuthManager authManager;
    public AdsManager ads;
    public GameObject gameOverScreen;
    public GameObject[] uiToHide;
    public TextManager textManager;
    public GameManager gameManager;
    public UpdateUser updateUser;

    // You lose
    public void GameOverScreen()
    {
        Time.timeScale = 0f;

        // Show ads
        ads.ShowBanner();
        ads.PlayInterstitialAd();

        // Game over scores
        float score = gameManager.GetScore();
        int bestScore = gameManager.GetBestScore();

        textManager.SetGameOverText(score, bestScore);
        gameOverScreen.SetActive(true);

        // Save new best score
        if (score > bestScore) SaveBestScore(score);

        HideUI();
        DestroyObstacles();
    }

    #region Utils
    // Save new best score
    void SaveBestScore(float score)
    {
        int bestScore = Mathf.RoundToInt(score);

        if (authManager.IsUserLoggedIn())
            updateUser.UpdateUserScore(bestScore);

        string bestScoreKey = gameManager.GetBestScoreKey();
        PlayerPrefs.SetInt(bestScoreKey, bestScore);
    }

    // Hide UI
    void HideUI()
    {
        foreach (GameObject ui in uiToHide)
        {
            ui.SetActive(false);
        }
    }

    // Destroy all obstacles
    void DestroyObstacles()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == LayerMask.NameToLayer("GameElements"))
            {
                Destroy(obj);
            }
        }
    }
    #endregion

    #region Game Over Buttons
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
