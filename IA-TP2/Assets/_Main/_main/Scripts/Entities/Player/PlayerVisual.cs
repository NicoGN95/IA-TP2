using UnityEngine;

namespace _Main._main.Scripts.Entities.Player
{
    public class PlayerVisual : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private static readonly int SpeedAnimId = Animator.StringToHash("Speed");
        //TODO = pasar todas los datos del animator a una data
        
        
        //TODO, descubrir como funciona el hash del animator



        public void PlayAnim(string p_animationName) => animator.Play(p_animationName);

        public void BlendAnimations(string p_animationNameStart, string p_animationNameFinish)
        {
        }
        public void SetAnimSpeed(float p_value)
        {
            if (animator != default)
                animator.SetFloat(SpeedAnimId, p_value);
        }
    }
}