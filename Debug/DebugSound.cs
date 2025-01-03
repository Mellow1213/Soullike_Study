using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DebugSound : MonoBehaviour
{
    public AudioClip testClip;
    private AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void PlayTestClip()
    {
        _audioSource.PlayOneShot(testClip);
    }
}
