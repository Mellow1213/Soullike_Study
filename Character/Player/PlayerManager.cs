using UnityEngine;

namespace SG
{
    public class PlayerManager : CharacterManager
    {
        private PlayerLocomotionManager _playerLocomotionManager;
        protected override void Awake()
        {
            base.Awake();
            _playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        }

        protected override void Update()
        {
            base.Update();
            if (!IsOwner) return;
            _playerLocomotionManager.HandleAllMovement();
        }
    }
}