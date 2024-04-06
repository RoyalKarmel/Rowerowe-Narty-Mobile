using UnityEngine;

public class BoostEffect : MonoBehaviour
{
    public BoostManager boostManager;

    public GameManager gameManager;
    public TextManager textManager;
    public Shooting shootingManager;
    public PlayerController playerController;
    public ParticleSystem explosionPrefab;

    public int boostDuration = 5;

    // Score multiplier
    public void Multiplier()
    {
        gameManager.SetScoreMultiplier(2);
        Invoke("ResetMultiplier", boostDuration);
    }

    public void ResetMultiplier()
    {
        boostManager.DisableBoost("Multiplier");
    }

    // Speed
    public void Speed()
    {
        playerController.SetAcceleration(true);
        Invoke("ResetSpeed", boostDuration);
    }

    public void ResetSpeed()
    {
        boostManager.DisableBoost("Speed");
    }

    // Shield
    public void Shield()
    {
        playerController.SetShield(true);
    }

    // Bomb
    public void Bomb(GameObject bomb)
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (GameObject obstacle in obstacles)
        {
            Destroy(obstacle);
        }

        Instantiate(explosionPrefab, bomb.transform.position, Quaternion.identity);

        var bombDuration = explosionPrefab.main.duration;

        Invoke("ResetBomb", bombDuration);
    }

    public void ResetBomb()
    {
        boostManager.DisableBoost("Bomb");
    }

    // Pistol
    public void Pistol()
    {
        shootingManager.ammo = 20;
        textManager.SetAmmoText(shootingManager.ammo);
    }
}
