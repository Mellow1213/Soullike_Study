using System;
using UnityEngine;

namespace SG
{
    public class PlayerCamera : MonoBehaviour
    {
        public static PlayerCamera instance;

        public Camera cameraObject;
        private Transform target;
        
        [Header("Camera Setting")] [SerializeField]
        private Vector3 cameraVelocity;

        [SerializeField] private float minClamp = -60.0f;
        [SerializeField] private float maxClamp = 75.0f;
        [SerializeField] private float cameraSmoothSpeed = 1;
        [SerializeField] private Transform rightPivot;
        [SerializeField] private Transform upPivot;
        public float sensitivity = 0.1f;

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

        public void SetFollowTarget(Transform t)
        {
            target = t;
        }

        public void ControlCamera()
        {
            // Follow Player
            FollowPlayer();
            // Rotate Around Player by Mouse
             RotatePlayer();
            // Block by Obstacle (like wall)
        }

        void FollowPlayer()
        {
            Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, target.position,
                ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
            transform.localPosition = targetCameraPosition;
        }

        private float rotationX = 0;
        private float rotationY = 0;
        void RotatePlayer()
        {
            Vector2 vec = InputManager.instance.GetMouse();
            rotationX += vec.x;
            rotationY += vec.y;

            rotationY = Mathf.Clamp(rotationY, minClamp, maxClamp);
            rightPivot.localRotation = Quaternion.Euler(0, rotationX, 0);
            upPivot.localRotation = Quaternion.Euler(-rotationY, 0, 0);
            Debug.Log(vec);
        }
    }
}