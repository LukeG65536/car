using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnBonk : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;

    private void Start()
    {
        audioSource.clip = clip;
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.Play();
    }
}
