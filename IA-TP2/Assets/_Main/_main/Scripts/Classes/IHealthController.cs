using _Main._main.Scripts.Entities;

namespace _Main._main.Scripts.Classes
{
    public interface IHealthController
    {
        void HitToModel<T>(T p_attacker, float p_damage) where T : BaseModel;
        void DoDamage(float p_damage);
        void DoHeal(float p_health);
        void Die();
    }
}