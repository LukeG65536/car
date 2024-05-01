using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnBonk : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;

    private void Start()
    {
        clip.LoadAudioData();
        audioSource.clip = clip;
    }

    private void Update()
    {
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Playing");
        audioSource.Play();
    }
}
