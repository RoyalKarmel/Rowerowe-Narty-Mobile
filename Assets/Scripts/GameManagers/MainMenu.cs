using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator logo;
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
