using System;
using UnityEngine;

namespace SG
{
    public class CharacterAnimationManager : MonoBehaviour
    {
        [HideInInspector] public CharacterManager _characterManager;
        protected virtual void Awake()
        {
            _characterManager = GetComponent<CharacterManager>();
        }

        public virtual void UpdateAllAnimation(float horizontalValue, float verticalValue)
        {
            Debug.Log("호출됨");
            _characterManager._animator.SetFloat("Horizontal", horizontalValue, 0.1f, Time.deltaTime*0.5f);
            _characterManager._animator.SetFloat("Vertical", verticalValue, 0.1f, Time.deltaTime*0.5f);
        }
    }
}