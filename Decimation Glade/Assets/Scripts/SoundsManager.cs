using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource = null;
    [SerializeField] private List<AudioClip> _soundEffects = new List<AudioClip>();

    [SerializeField] private float _timeBetweenSounds = 6f;

    private float _timer = 0;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _timeBetweenSounds)
        {
            _timer = 0;

            if (_audioSource != null && _soundEffects.Count > 0)
                _audioSource.PlayOneShot(GetRandomSoundEffect());
        }
    }

    private AudioClip GetRandomSoundEffect()
    {
        return _soundEffects[Random.Range(0, _soundEffects.Count)];
    }
}
