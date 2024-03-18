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

    public GameManager gameManager;
    public PlayerController playerController;

    public int boostDuration = 5;
    public int ammo = 20;
    public float acceleration = 9f;

    // Show boost UI & play sound
    public void boostEffect(string boostTag)
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
                Pistol();
                break;
            default:
                break;
        }
    }

    // Hide boost UI
    public void hideBoostUI(string boostTag)
    {
        switch (boostTag)
        {
            case "Multiplier":
                multiplierImage.SetActive(false);
                break;
            case "Speed":
                speedImage.SetActive(false);
                break;
            case "Shield":
                shieldImage.SetActive(false);
                soundManager.PlayBoostSound(boostTag);
                break;
            case "Pistol":
                pistolImage.SetActive(false);
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
        gameManager.scoreMultiplier = 1;
        hideBoostUI("Multiplier");
    }

    // Speed
    void Speed()
    {
        playerController.SetMoveSpeed(acceleration);
        Invoke("ResetSpeed", boostDuration);
    }

    void ResetSpeed()
    {
        playerController.SetMoveSpeed();
        hideBoostUI("Speed");
    }

    // Shield
    void Shield()
    {
        playerController.ToggleShield(true);
    }

    // Pistol
    void Pistol()
    {
        gameManager.SetAmmoText(ammo);
    }

    void Shoot()
    {
        ammo--;
        AudioSource.PlayClipAtPoint(soundManager.shoot, transform.position);

        if (ammo == 0) hideBoostUI("Pistol");

        gameManager.SetAmmoText(ammo);
    }
}
