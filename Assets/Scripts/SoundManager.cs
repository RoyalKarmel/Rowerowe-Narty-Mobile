using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip multiplier;
    public AudioClip speed;
    public AudioClip shield;
    public AudioClip pistol;
    public AudioClip coin;

    public void PlayBoostSound(string boostTag)
    {
        switch (boostTag)
        {
            case "Multiplier":
                AudioSource.PlayClipAtPoint(multiplier, transform.position);
                break;
            case "Speed":
                AudioSource.PlayClipAtPoint(speed, transform.position);
                break;
            case "Shield":
                AudioSource.PlayClipAtPoint(shield, transform.position);
                break;
            case "Pistol":
                AudioSource.PlayClipAtPoint(pistol, transform.position);
                break;
            case "Coin":
                AudioSource.PlayClipAtPoint(coin, transform.position);
                break;
            default:
                break;
        }
    }
}
