using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

namespace SG
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager instance;
        public PlayerManager _playerManager;
        private PlayerInput _playerInput;
        [SerializeField] private Vector2 movementInput;
        [SerializeField] private Vector2 mouseInput;
        [SerializeField] private bool sprintInput;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this; // 싱글톤 패턴 적용
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.activeSceneChanged += OnSceneChange; // 씬이 active될 때의 이벤트에 등록
            instance.enabled = false;
        }

        void OnSceneChange(Scene oldScene, Scene newScene)
        {
            if (newScene.buildIndex == WorldSaveGameManager.instance.GetWorldSceneIndex()) // 정해진 씬으로 가면 Input 허용
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
                _playerInput.PlayerMovement.Sprint.performed += i => sprintInput = true;
                _playerInput.PlayerMovement.Sprint.canceled += i => sprintInput = false;
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

        public bool GetSprint()
        {
            return sprintInput;
        }
    }
}