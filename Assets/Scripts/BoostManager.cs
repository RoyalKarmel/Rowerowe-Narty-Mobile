using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostManager : MonoBehaviour
{
    public SoundManager soundManager;
    public GameObject multiplierImage;
    public GameObject speedImage;
    public GameObject shieldImage;
    public GameObject pistolImage;

    public void showBoostUI(string boostTag)
    {
        soundManager.PlayBoostSound(boostTag);

        switch (boostTag)
        {
            case "Multiplier":
                multiplierImage.SetActive(true);
                break;
            case "Speed":
                speedImage.SetActive(true);
                break;
            case "Shield":
                shieldImage.SetActive(true);
                break;
            case "Pistol":
                pistolImage.SetActive(true);
                break;
            default:
                break;
        }
    }
}
