using System;
using UnityEngine;

namespace SG
{
    public class CharacterAnimationManager : MonoBehaviour
    {
        [HideInInspector] public CharacterManager _characterManager;
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private bool isPerformingAction = false;
        protected virtual void Awake()
        {
            _characterManager = GetComponent<CharacterManager>();
        }

        public virtual void UpdateAllAnimation(float horizontalValue, float verticalValue)
        {
            _characterManager._animator.SetFloat(Horizontal, horizontalValue, 0.1f, Time.deltaTime);
            _characterManager._animator.SetFloat(Vertical, verticalValue, 0.1f, Time.deltaTime);
        }

        public virtual void UpdateRollAnimation(string targetAnimation, bool actionState, bool applyRootMotion = true)
        {
            _characterManager._animator.applyRootMotion = applyRootMotion;
            _characterManager._animator.CrossFade(targetAnimation, 0.2f);
            isPerformingAction = actionState;
        }
    }
}