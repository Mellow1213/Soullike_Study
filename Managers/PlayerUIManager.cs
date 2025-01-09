using System;
using UnityEngine;
using Unity.Netcode;
namespace SG
{
    public class PlayerUIManager : MonoBehaviour
    {
        public static PlayerUIManager instance;
        [Header("NETWORK JOIN")] 
        [SerializeField] private bool startGameAsClient;

        public PlayerUIHudManager _playerUIHUDManager;
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

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (startGameAsClient)
            {
                startGameAsClient = false;
                // 타이틀에서 한번 START 했으므로 우선 SHUTDOWN
                NetworkManager.Singleton.Shutdown();
                // 클라이언트로 다시 START
                NetworkManager.Singleton.StartClient();
            }
        }
    }
}
