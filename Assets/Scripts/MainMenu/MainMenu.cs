using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public AdsManager ads;
    public Animator logo;

    void Start()
    {
        ads.ShowBanner();

        PlayerPrefs.SetInt("Skin0", 1);
        PlayerPrefs.SetInt("Music0", 1);
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
