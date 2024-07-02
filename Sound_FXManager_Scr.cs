using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_FXManager_Scr : MonoBehaviour
{
    public static Sound_FXManager_Scr instance;

    [SerializeField] private AudioSource soundFXPrefab;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void PlayRandomFXClip(AudioClip[] audioClips, Transform spawnTransform, float volume)
    {
        int rand = Random.Range(0, audioClips.Length);
        AudioSource audioSource = Instantiate(soundFXPrefab, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClips[rand];
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioClips[rand].length;
        Destroy(audioSource.gameObject, clipLength);
    }
}
