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

        protected virtual void Start()
        {
            
        }
        protected virtual void Update()
        {
            SyncTransform();
        }

        private void SyncTransform()
        {
            if (IsOwner) // 내 캐릭터면 네트워크 포지션이 이곳이라고 송신
            {
                _characterNetworkManager.networkPosition.Value = transform.position;
                _characterNetworkManager.networkRotation.Value = transform.rotation;
            }
            else // 남의 캐릭터면 네트워크 포지션을 수신받아 값 변경
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