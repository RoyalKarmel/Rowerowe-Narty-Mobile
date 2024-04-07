using UnityEngine;

public class BoostManager : MonoBehaviour
{
    public BoostEffect boostEffect;

    public GameObject multiplierImage;
    public GameObject speedImage;
    public GameObject shieldImage;
    public GameObject bombImage;
    public GameObject[] pistolImages;

    public SoundEffects soundEffects;
    public GameManager gameManager;
    public PlayerController playerController;
    public GameObject shootingKeys;

    // Show boost UI & play sound
    public void BoostEffect(GameObject boost)
    {
        soundEffects.PlayBoostSound(boost.tag);

        switch (boost.tag)
        {
            case "Multiplier":
                multiplierImage.SetActive(true);
                boostEffect.Multiplier();
                break;
            case "Speed":
                speedImage.SetActive(true);
                boostEffect.Speed();
                break;
            case "Shield":
                shieldImage.SetActive(true);
                boostEffect.Shield();
                break;
            case "Bomb":
                bombImage.SetActive(true);
                boostEffect.Bomb(boost);
                break;
            case "Pistol":
                foreach (GameObject pistolImg in pistolImages)
                {
                    pistolImg.SetActive(true);
                }
                shootingKeys.SetActive(true);
                boostEffect.Pistol();
                break;
            case "Coin":
                gameManager.SetCoins(1);
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
                gameManager.SetScoreMultiplier(1);
                break;
            case "Speed":
                speedImage.SetActive(false);
                playerController.SetAcceleration(false);
                break;
            case "Shield":
                shieldImage.SetActive(false);
                soundEffects.PlayBoostSound(boostTag);
                break;
            case "Bomb":
                bombImage.SetActive(false);
                break;
            case "Pistol":
                foreach (GameObject pistolImg in pistolImages)
                {
                    pistolImg.SetActive(true);
                }
                shootingKeys.SetActive(false);
                break;
            default:
                break;
        }
    }
}
