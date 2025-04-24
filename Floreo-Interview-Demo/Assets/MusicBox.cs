using System;
using UnityEngine;

// Single Responsibility Principle: This class is only responsible for handling the interactions with the music box. (Playing and stopping music.)
public class MusicBox : MonoBehaviour, IInteractable
{
    private AudioSource _audioSource;
    private Outline _outline;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _outline = GetComponent<Outline>();
        _audioSource.loop = true; // Set the audio source to loop
        _audioSource.playOnAwake = false;
    }
    
    public void Interact()
    {
        _outline.enabled = true;
        if (_audioSource != null)
        {
            _audioSource.Play();
        }
    }
    
    public void Uninteract()
    {
        _outline.enabled = false;
        if (_audioSource != null)
        {
            _audioSource.Stop();
        }
    }
}
