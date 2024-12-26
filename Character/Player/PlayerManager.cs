using System;
using UnityEngine;

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
    }
}