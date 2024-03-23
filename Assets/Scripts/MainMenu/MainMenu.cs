using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AdsManager ads;
    public Animator logo;
    public TMP_Text coinsText;

    private string coinsKey = "Coins";

    void Start()
    {
        ads.ShowBanner();
        
        PlayerPrefs.SetInt("Skin0", 1);
        PlayerPrefs.SetInt("Music0", 1);

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
