using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeowManager : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] List<AudioClip> _meows;
    private void Start()
    {
        InvokeRepeating(nameof(PlayRandomMeow), 5f, 10f);
    }

    private void PlayRandomMeow()
    {
        int randomIndex = Random.Range(0, _meows.Count);
        AudioClip randomMeow = _meows[randomIndex];
        _audioSource.PlayOneShot(randomMeow);
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
}