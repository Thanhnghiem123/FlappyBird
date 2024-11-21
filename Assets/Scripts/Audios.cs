using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IAudioManager
{
    void PlayDieSound();
    void PlayHitSound();
    void PlayPointSound();
    void PlaySwooshSound();
    void PlayWingSound();
}
public class Audios : MonoBehaviour, IAudioManager
{
    public AudioClip dieClip;
    public AudioClip hitClip;
    public AudioClip pointClip;
    public AudioClip swooshClip;
    public AudioClip wingClip;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayDieSound()
    {
        PlaySound(dieClip);
    }

    public void PlayHitSound()
    {
        PlaySound(hitClip);
    }

    public void PlayPointSound()
    {
        PlaySound(pointClip);
    }

    public void PlaySwooshSound()
    {
        PlaySound(swooshClip);
    }

    public void PlayWingSound()
    {
        PlaySound(wingClip);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Audio clip is missing!");
        }
    }
}
