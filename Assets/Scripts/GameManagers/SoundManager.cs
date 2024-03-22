using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] musics;
    public AudioClip multiplier;
    public AudioClip speed;
    public AudioClip shield;
    public AudioClip pistol;
    public AudioClip shoot;
    public AudioClip coin;

    private AudioSource audioSource;
    private string selectedMusicKey = "SelectedMusicID";

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        int selectedMusicID = PlayerPrefs.GetInt(selectedMusicKey, 0);
        if (musics != null && selectedMusicID >= 0 && selectedMusicID < musics.Length)
        {
            audioSource.clip = musics[selectedMusicID];
            audioSource.Play();
        }
        else
            Debug.LogError("Invalid music configuration or selected music ID: " + selectedMusicID);
    }

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

    public void PlayShootSound()
    {
        AudioSource.PlayClipAtPoint(shoot, transform.position);
    }
}
