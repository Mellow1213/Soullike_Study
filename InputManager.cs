using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace SG
{
    public class InputManager : MonoBehaviour
    {
        private PlayerInput _playerInput;
        [FormerlySerializedAs("movement")] [SerializeField] private Vector2 movementInput;
        private void OnEnable()
        {
            if (_playerInput == null)
            {
                _playerInput = new PlayerInput();
                _playerInput.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
                _playerInput.Enable();
            }
        }
    }
}