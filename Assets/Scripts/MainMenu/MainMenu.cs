using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator logo;
    public TMP_Text coinsText;

    private string coinsKey = "Coins";

    void Start()
    {
        logo.Play("LogoStart");
        PlayerPrefs.SetInt("Skin0", 1);

        int coins = PlayerPrefs.GetInt(coinsKey, 0);
        coinsText.text = coins.ToString();
    }

    public void PlayGame()
    {
        logo.Play("LogoPlay");
        Invoke("StartGame", 2.5f);
    }

    void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
