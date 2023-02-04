using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource = null;
    [SerializeField] private AudioClip _musicClip = null;

    private void Start()
    {
        if (_audioSource != null || _musicClip != null)
            _audioSource.PlayOneShot(_musicClip);
    }
}
