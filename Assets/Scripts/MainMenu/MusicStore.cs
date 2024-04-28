using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicStore : MonoBehaviour
{
    public AudioSource soundManager;
    private AudioSource currentMusic;

    public void PlayStoreMusic()
    {
        Button clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        if (clickedButton == null)
        {
            Debug.LogWarning("No button clicked!");
            return;
        }

        AudioSource newMusic = clickedButton.GetComponentInChildren<AudioSource>();

        if (currentMusic != null && currentMusic.isPlaying)
            currentMusic.Stop();

        currentMusic = newMusic;

        if (soundManager.isPlaying)
            soundManager.volume = 0;
        newMusic.Play();

        StartCoroutine(WaitForMusicToEnd());
    }

    IEnumerator WaitForMusicToEnd()
    {
        while (currentMusic.isPlaying)
            yield return null;

        soundManager.volume = 1;
    }
}
