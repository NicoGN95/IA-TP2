using _Main._main.Scripts.Classes;
using _Main._main.Scripts.Datas;
using UnityEngine;

namespace _Main._main.Scripts.Entities
{
    public class BaseModel : MonoBehaviour, IHealthController
    {
        [SerializeField] private BaseData baseData;
        private HealthController m_healthController;
        
        private void Awake()
        {
            m_healthController = new HealthController(baseData.MaxHealth);
            m_healthController.OnDie += Die;
        }

        public virtual void HitToModel<T>(T p_attacker, float p_damage) where T : BaseModel{}
        public virtual void Die() {}
        public void DoDamage(float p_damage)
        {
            m_healthController.TakeDamage(p_damage);
        }

        public void DoHeal(float p_health)
        {
            m_healthController.Heal(p_health);
        }

    }
}