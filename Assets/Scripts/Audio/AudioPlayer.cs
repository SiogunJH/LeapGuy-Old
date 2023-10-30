using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClipsPool;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        audioSource.clip = audioClipsPool[Random.Range(0, audioClipsPool.Length)];
        audioSource.Play();
    }
}
