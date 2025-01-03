using System;
using UnityEngine;

namespace SG
{
    public class PlayerSoundManager : CharacterSoundManager
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayRollSound()
        {
            _audioSource.PlayOneShot(SoundContainer.instance.rollSFX);
        }
    }
}