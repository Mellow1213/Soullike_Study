using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace SG
{
    public class WorldSaveGameManager : MonoBehaviour
    {
        public static WorldSaveGameManager instance;
        [SerializeField] private int worldSceneIndex = 1;
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
        }

        public IEnumerator LoadNewGame()
        {
            AsyncOperation loadOperator = SceneManager.LoadSceneAsync(worldSceneIndex);
            yield return null;
        }
    }
}