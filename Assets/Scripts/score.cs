using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class score : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text bestScoreText;

    private int currentScore = 0;
    private int currentBestScore = 0;
    private string bestScoreKey = "BestScore";

    void Start()
    {
        if (PlayerPrefs.HasKey(bestScoreKey))
        {
            currentBestScore = PlayerPrefs.GetInt(bestScoreKey);
            SetBestScoreText();
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
}
