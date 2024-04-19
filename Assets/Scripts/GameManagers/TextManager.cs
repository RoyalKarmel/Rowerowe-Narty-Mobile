using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    [Header("Text Elements")]
    public TMP_Text scoreText;
    public TMP_Text bestScoreText;
    public TMP_Text gameOverScoreText;
    public TMP_Text gameOverBestScoreText;
    public TMP_Text coinsText;
    public TMP_Text ammoText;

    // Set scores text
    public void SetBestScoreText(int bestScore)
    {
        bestScoreText.text = "Best Score: <color=#f21010>" + bestScore.ToString() + "</color>";
    }

    public void SetScoreText(float currentScore)
    {
        scoreText.text = "Score: <color=#f21010>" + Mathf.RoundToInt(currentScore).ToString() + "</color>";
    }

    public void SetGameOverText(float currentScore, int bestScore)
    {
        gameOverScoreText.text = "Score: <color=#f21010>" + Mathf.RoundToInt(currentScore).ToString() + "</color>";
        gameOverBestScoreText.text = "Best Score: <color=#f21010>" + bestScore.ToString() + "</color>";
    }

    // Set coins text
    public void SetCoinsText(int coins)
    {
        coinsText.text = coins.ToString();
    }

    // Set ammo text
    public void SetAmmoText(int ammo)
    {
        ammoText.text = ammo.ToString();
    }
}
