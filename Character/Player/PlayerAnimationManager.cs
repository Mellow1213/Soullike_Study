using UnityEngine;

namespace SG
{
    public class PlayerAnimationManager : CharacterAnimationManager
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }
        
        public override void UpdateAllAnimation(float horizontalValue, float verticalValue)
        {
            Debug.Log("호출");
            base.UpdateAllAnimation(horizontalValue, verticalValue);
        }
    }
}