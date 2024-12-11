using System;
using UnityEngine;

namespace SG
{
    public class PlayerCamera : MonoBehaviour
    {
        public static PlayerCamera instance;

        public Camera cameraObject;
        public PlayerManager _playerManager;

        [Header("Camera Setting")] 
        [SerializeField] private Vector3 cameraVelocity;
        [SerializeField] private float cameraSmoothSpeed = 1;
        [SerializeField] private Vector3 offset = new Vector3(3, 3, 3);

        private void Awake()
        {
            if (instance == null)
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
            DontDestroyOnLoad(this);
        }

        public void ControlCamera()
        {
            // Follow Player
            FollowPlayer();
            // Rotate Around Player by Mouse
            // Block by Obstacle (like wall)
        }

        void FollowPlayer()
        {
            Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, _playerManager.transform.position + offset,
                ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
            transform.position = targetCameraPosition;
        }
    }
}