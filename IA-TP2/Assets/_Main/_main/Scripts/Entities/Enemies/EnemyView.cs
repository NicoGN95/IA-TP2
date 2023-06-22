using UnityEngine;

namespace _Main._main.Scripts.Entities.Enemies
{
    public class EnemyView: MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public void PlayIdleAnimation()
        {
            
        }

        public void SetCombatAnimation(bool p_isInCombat)
        {
            animator.SetBool("IsInCombat", p_isInCombat);
        }

        public void PlayMovementCombatAnim(Vector3 p_dir)
        {
            animator.SetFloat("Speed", p_dir.magnitude);
            animator.SetFloat("DirX", p_dir.x);
            animator.SetFloat("DirZ", p_dir.z);
        }
        public void PlayMovementAnimation(float p_speed)
        {
            animator.SetFloat("Speed", p_speed);
        }
        
        
        public void PlayBlockingAnimation()
        {
            
        }

        public void PlayStunAnimation()
        {
            animator.Play("Stun");
        }
        public void PlayAttackAnimation(string p_animationName)
        {
            animator.Play(p_animationName);
        }

        public void PlayDieAnimation()
        {
            
        }
    }
}