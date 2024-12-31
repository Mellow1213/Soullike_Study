using System;
using UnityEngine;

namespace SG
{
    public class PlayerCamera : MonoBehaviour
    {
        public static PlayerCamera instance;

        public Camera cameraObject;
        private Transform target;
        [SerializeField] private Transform cameraPivotTransform;
        [Header("Camera Setting")] [SerializeField]
        private Vector3 cameraVelocity;
        
        [SerializeField] private float minClamp = -60.0f;
        [SerializeField] private float maxClamp = 75.0f;
        [SerializeField] private float cameraSmoothSpeed = 1;
        [SerializeField] private Transform rightPivot;
        [SerializeField] private Transform upPivot;
        public float sensitivity = 0.1f;
        [SerializeField] private float cameraCollisionPivot = 0.2f;
        private Vector3 cameraObjectPosition;
        private float cameraZPosition;
        private float targetCameraZPosition;
        private float cameraCollisionRadius = 0.2f;
        [SerializeField] private LayerMask collideWithLayers;

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
            cameraZPosition = cameraObject.transform.localPosition.z;
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
            CheckCollision();
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
        }

        void CheckCollision()
        {
            targetCameraZPosition = cameraZPosition;
            RaycastHit hit;
            Vector3 direction = cameraObject.transform.position - cameraPivotTransform.position;
            Debug.DrawRay(cameraPivotTransform.position, direction, Color.red);
            direction.Normalize();
            if (Physics.SphereCast(cameraPivotTransform.position, cameraCollisionRadius, direction, out hit,
                    Mathf.Abs(targetCameraZPosition), collideWithLayers))
            {
                float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.position, hit.point);
                targetCameraZPosition = -(distanceFromHitObject - cameraCollisionRadius);
            }

            if (Mathf.Abs(targetCameraZPosition) < cameraCollisionRadius)
            {
                targetCameraZPosition = -cameraCollisionRadius;
            }

            cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, targetCameraZPosition, 0.2f);
            cameraObject.transform.localPosition = cameraObjectPosition;
        }
    }
}