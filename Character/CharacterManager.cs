using System;
using Unity.Netcode;
using UnityEngine;

namespace SG
{
    public class CharacterManager : NetworkBehaviour
    {
        private CharacterController _characterController;
        private CharacterNetworkManager _characterNetworkManager;
        protected virtual void Awake()
        {
            DontDestroyOnLoad(this);

            _characterController = GetComponent<CharacterController>();
            _characterNetworkManager = GetComponent<CharacterNetworkManager>();
        }

        protected virtual void Update()
        {
            if (IsOwner)
            {
                _characterNetworkManager.networkPosition.Value = transform.position;
                _characterNetworkManager.networkRotation.Value = transform.rotation;
            }
            else
            {
                transform.position = Vector3.SmoothDamp(
                    transform.position,
                    _characterNetworkManager.networkPosition.Value, 
                    ref _characterNetworkManager.networkPositionVelocity, 
                    _characterNetworkManager.networkPositionSmoothTime);
                
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    _characterNetworkManager.networkRotation.Value, 
                    _characterNetworkManager.networkRotationSmoothTime);
            }
        }
    }
}