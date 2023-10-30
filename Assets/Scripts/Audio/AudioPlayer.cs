using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClipsPool;
    [SerializeField] AudioSource audioSource;
    public void PlaySound()
    {
        audioSource.clip = audioClipsPool[Random.Range(0, audioClipsPool.Length)];
        audioSource.Play();
    }
}
