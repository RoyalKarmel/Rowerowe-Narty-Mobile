using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] musics;
    public AudioClip multiplier;
    public AudioClip speed;
    public AudioClip shield;
    public AudioClip pistol;
    public AudioClip shoot;
    public AudioClip coin;
    public AudioClip bomb;

    private string selectedMusicKey = "SelectedMusicID";

    void Start()
    {
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
