using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform player;
    public GameManager gameManager;
    public SoundManager soundManager;
    public BoostManager boostManager;

    public int ammo = 20;

    public void ShootUp()
    {
        Shoot(0f);
    }
    public void ShootDown()
    {
        Shoot(180f);
    }
    public void ShootLeft()
    {
        Shoot(90f);
    }
    public void ShootRight()
    {
        Shoot(270f);
    }

    void Shoot(float rotationAngle)
    {
        ammo--;
        soundManager.PlayShootSound();
        gameManager.SetAmmoText(ammo);

        Vector3 startPosition = player.position;
        Instantiate(bulletPrefab, startPosition, Quaternion.Euler(0, 0, rotationAngle));

        if (ammo == 0) boostManager.DisableBoost("Pistol");
    }
}
