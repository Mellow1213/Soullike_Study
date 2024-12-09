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
                _playerInput.Enable();
            }
        }

        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= OnSceneChange;
        }

        public Vector2 GetMove()
        {
            return instance.isActiveAndEnabled ? movementInput : Vector2.zero;
        }
    }
}