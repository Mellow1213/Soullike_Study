using UnityEngine;

namespace SG
{
    public class SoundContainer : MonoBehaviour
    {
        public static SoundContainer instance;

        [Header("Action Sounds")] public AudioClip rollSFX;
        void Awake()
        {
            if (instance is null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
