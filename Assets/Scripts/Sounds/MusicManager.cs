using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] musics;

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
}
