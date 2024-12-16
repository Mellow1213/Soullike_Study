using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

namespace SG
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager instance;
        private PlayerInput _playerInput;
        [SerializeField] private Vector2 movementInput;
        [SerializeField] private Vector2 mouseInput;

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
            DontDestroyOnLoad(gameObject);
            SceneManager.activeSceneChanged += OnSceneChange;
            instance.enabled = false;
        }

        void OnSceneChange(Scene oldScene, Scene newScene)
        {
            if (newScene.buildIndex == WorldSaveGameManager.instance.GetWorldSceneIndex())
            {
                instance.enabled = true;
            }
            else
            {
                instance.enabled = false;
            }
        }

        private void OnEnable()
        {
            if (_playerInput == null)
            {
                _playerInput = new PlayerInput();
                _playerInput.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
                _playerInput.PlayerMovement.Mouse.performed  += i => mouseInput = i.ReadValue<Vector2>();
                _playerInput.PlayerMovement.Mouse.canceled  += i => mouseInput = Vector2.zero;
                _playerInput.Enable();
            }
        }

        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= OnSceneChange;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (enabled)
            {
                if (hasFocus)
                {
                    _playerInput.Enable();
                }
                else
                {
                    _playerInput.Disable();
                }
            }
        }

        public Vector2 GetMove()
        {
            return instance.isActiveAndEnabled ? movementInput : Vector2.zero;
        }

        public Vector2 GetMouse()
        {
            return instance.isActiveAndEnabled ? mouseInput : Vector2.zero;
        }
    }
}