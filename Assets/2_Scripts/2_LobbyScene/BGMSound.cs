using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMSound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.playOnAwake = true;

        audioSource.Play();
    }
}
