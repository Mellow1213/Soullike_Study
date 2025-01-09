using System;
using UnityEngine;
using UnityEngine.XR;

namespace SG
{
    public class PlayerManager : CharacterManager
    {
        [HideInInspector] public PlayerLocomotionManager _playerLocomotionManager;
        [HideInInspector] public PlayerAnimationManager _playerAnimationManager;
        [SerializeField] private Transform cameraTarget;
        protected override void Awake()
        {
            base.Awake();
            _playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
            _playerAnimationManager = GetComponent<PlayerAnimationManager>();
        }

        protected override void Start()
        {
            InputManager.instance.OnDodge += HandleDodge;
        }

        protected override void Update()
        {
            base.Update();
            if (!IsOwner) return;
            _playerLocomotionManager.HandleAllMovement();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if (IsOwner)
            {
                PlayerCamera.instance.SetFollowTarget(cameraTarget);
                // PlayerUIManager.instance._playerUIHUDManager 
                InputManager.instance._playerManager = this;
            }
        }

        protected override void LateUpdate()
        {
            if (!IsOwner)
                return;
            base.LateUpdate();
            PlayerCamera.instance.ControlCamera();
        }

        private void HandleDodge()
        {
            if (!IsOwner) return;
            _playerLocomotionManager.AttemptToTryDodge();
        }
    }
}