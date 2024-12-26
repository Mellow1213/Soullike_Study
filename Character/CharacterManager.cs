using System;
using Unity.Netcode;
using UnityEngine;

namespace SG
{
    public class CharacterManager : NetworkBehaviour
    {
        private CharacterController _characterController;
        [HideInInspector] public CharacterNetworkManager _characterNetworkManager;

        [HideInInspector] public Animator _animator;
        protected virtual void Awake()
        {
            DontDestroyOnLoad(this);
            _characterController = GetComponent<CharacterController>();
            _characterNetworkManager = GetComponent<CharacterNetworkManager>();
            _animator = GetComponent<Animator>();
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

        protected virtual void LateUpdate()
        {
            //throw new NotImplementedException();
        }
    }
}