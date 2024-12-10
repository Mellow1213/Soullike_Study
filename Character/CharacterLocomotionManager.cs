using UnityEngine;

namespace SG
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterLocomotionManager : MonoBehaviour
    {
        protected CharacterController _characterController;
        protected virtual void Awake()
        {
            DontDestroyOnLoad(this);
            _characterController = GetComponent<CharacterController>();
        }

        protected virtual void Update()
        {
            
        }
    }
}