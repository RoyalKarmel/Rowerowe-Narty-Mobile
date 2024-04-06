using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip multiplier;
    public AudioClip speed;
    public AudioClip shield;
    public AudioClip pistol;
    public AudioClip shoot;
    public AudioClip coin;
    public AudioClip bomb;

    public void PlayBoostSound(string boostTag)
    {
        AudioClip clipToPlay = null;

        switch (boostTag)
        {
            case "Multiplier":
                clipToPlay = multiplier;
                break;
            case "Speed":
                clipToPlay = speed;
                break;
            case "Shield":
                clipToPlay = shield;
                break;
            case "Bomb":
                clipToPlay = bomb;
                break;
            case "Pistol":
                clipToPlay = pistol;
                break;
            case "Coin":
                clipToPlay = coin;
                break;
            default:
                break;
        }

        if (clipToPlay != null)
            audioSource.PlayOneShot(clipToPlay);
    }

    public void PlayShootSound()
    {
        AudioSource.PlayClipAtPoint(shoot, transform.position);
    }
}
