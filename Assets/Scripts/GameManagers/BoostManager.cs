using UnityEngine;

public class BoostManager : MonoBehaviour
{
    public GameObject multiplierImage;
    public GameObject speedImage;
    public GameObject shieldImage;
    public GameObject bombImage;
    public GameObject pistolImage;

    public SoundEffects soundEffects;
    public GameManager gameManager;
    public Shooting shootingManager;
    public PlayerController playerController;
    public GameObject shootingKeys;

    public int boostDuration = 5;
    public int bombDuration = 2;
    public float acceleration = 9f;

    // Show boost UI & play sound
    public void BoostEffect(string boostTag)
    {
        soundEffects.PlayBoostSound(boostTag);

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
            case "Bomb":
                bombImage.SetActive(true);
                Bomb();
                break;
            case "Pistol":
                pistolImage.SetActive(true);
                shootingKeys.SetActive(true);
                Pistol();
                break;
            case "Coin":
                gameManager.SetCoins(1);
                gameManager.collectedCoins++;
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
                soundEffects.PlayBoostSound(boostTag);
                break;
            case "Bomb":
                bombImage.SetActive(false);
                break;
            case "Pistol":
                pistolImage.SetActive(false);
                shootingKeys.SetActive(false);
                break;
            default:
                break;
        }
    }

    #region BoostEffects
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

    // Bomb
    void Bomb()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (GameObject obstacle in obstacles)
        {
            Destroy(obstacle);
        }

        Invoke("ResetBomb", bombDuration);
    }

    void ResetBomb()
    {
        DisableBoost("Bomb");
    }

    // Pistol
    void Pistol()
    {
        shootingManager.ammo = 20;
        gameManager.SetAmmoText(shootingManager.ammo);
    }
    #endregion
}
