using UnityEngine;

public class BoostEffect : MonoBehaviour
{
    public BoostManager boostManager;
    public GameManager gameManager;
    public TextManager textManager;
    public Shooting shootingManager;
    public PlayerController playerController;
    public ParticleSystem explosionPrefab;
    public ParticleSystem movementTraces;
    public int boostDuration = 5;

    #region  Score multiplier
    public void Multiplier()
    {
        gameManager.SetScoreMultiplier(2);
        Invoke("ResetMultiplier", boostDuration);
    }

    public void ResetMultiplier()
    {
        boostManager.DisableBoost("Multiplier");
    }
    #endregion

    #region  Speed
    public void Speed()
    {
        playerController.SetAcceleration(true);

        var emission = movementTraces.emission;
        emission.enabled = true;

        Invoke("ResetSpeed", boostDuration);
    }

    public void ResetSpeed()
    {
        var emission = movementTraces.emission;
        emission.enabled = false;

        boostManager.DisableBoost("Speed");
    }
    #endregion

    #region  Shield
    public void Shield()
    {
        playerController.SetShield(true);
    }
    #endregion

    #region  Bomb
    public void Bomb(GameObject bomb)
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Enemy");

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
    #endregion

    #region  Pistol
    public void Pistol()
    {
        shootingManager.ammo = 20;
        textManager.SetAmmoText(shootingManager.ammo);
    }
    #endregion
}
