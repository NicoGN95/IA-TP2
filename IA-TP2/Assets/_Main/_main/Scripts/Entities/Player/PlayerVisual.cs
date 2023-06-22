using UnityEngine;

namespace _Main._main.Scripts.Entities.Player
{
    public class PlayerVisual : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        //TODO = pasar todas los datos del animator a una data
        
        
        //TODO, descubrir como funciona el hash del animator
        private const string SPEED_ID = "Speed";



        public void PlayAnim(string p_animationName) => animator.Play(p_animationName);

        public void BlendAnimations(string p_animationNameStart, string p_animationNameFinish)
        {
        }
        public void SetAnimSpeed(float p_value) => animator.SetFloat(SPEED_ID, p_value);
    }
}