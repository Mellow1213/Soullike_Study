using UnityEngine;

namespace SG
{
    public class CharacterStatManager : MonoBehaviour
    {
        public void CalcStaminaByEnduranceLevel(int endurance)
        {
            float MaxStamina = endurance * 10f;
        }
    }
}