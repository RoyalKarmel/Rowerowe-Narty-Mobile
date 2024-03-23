using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostManager : MonoBehaviour
{
    public SoundManager soundManager;
    public GameObject multiplierImage;
    public GameObject speedImage;
    public GameObject shieldImage;
    public GameObject pistolImage;

    public GameManager gameManager;
    public Shooting shootingManager;
    public PlayerController playerController;
    public GameObject shootingKeys;

    public int boostDuration = 5;
    public float acceleration = 9f;

    // Show boost UI & play sound
    public void BoostEffect(string boostTag)
    {
        soundManager.PlayBoostSound(boostTag);

        switch (boostTag)
        {
            case "Multiplier":
                multiplierImage.SetActive(true);
                Multiplier();
                break;
            case "Speed":
                speedImage.SetActive(true);
                Speed();
                break;
            case "Shield":
                shieldImage.SetActive(true);
                Shield();
                break;
            case "Pistol":
                pistolImage.SetActive(true);
                shootingKeys.SetActive(true);
                Pistol();
                break;
            case "Coin":
                gameManager.SetCoins();
                break;
            default:
                break;
        }
    }

    // Hide boost UI
    public void DisableBoost(string boostTag)
    {
        switch (boostTag)
        {
            case "Multiplier":
                multiplierImage.SetActive(false);
                gameManager.scoreMultiplier = 1;
                break;
            case "Speed":
                speedImage.SetActive(false);
                playerController.SetMoveSpeed();
                break;
            case "Shield":
                shieldImage.SetActive(false);
                soundManager.PlayBoostSound(boostTag);
                break;
            case "Pistol":
                pistolImage.SetActive(false);
                shootingKeys.SetActive(false);
                break;
            default:
                break;
        }
    }

    // Score multiplier
    void Multiplier()
    {
        gameManager.scoreMultiplier = 2;
        Invoke("ResetMultiplier", boostDuration);
    }

    void ResetMultiplier()
    {
        DisableBoost("Multiplier");
    }

    // Speed
    void Speed()
    {
        playerController.SetMoveSpeed(acceleration);
        Invoke("ResetSpeed", boostDuration);
    }

    void ResetSpeed()
    {
        DisableBoost("Speed");
    }

    // Shield
    void Shield()
    {
        playerController.ToggleShield(true);
    }

    // Pistol
    void Pistol()
    {
        shootingManager.ammo = 20;
        gameManager.SetAmmoText(shootingManager.ammo);
    }
}
