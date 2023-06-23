using _Main._main.Scripts.Entities;
using UnityEngine;

namespace _Main._main.Scripts.Classes.SteeringBhb.Steering_Behaviours
{
    public class Evade : ISteeringBehaviour
    {
        private Transform m_origin;
        private BaseModel m_target;
        private float m_time;
        
        public Evade(Transform p_origin, BaseModel p_target, float p_time)
        {
            m_origin = p_origin;
            m_target = p_target;
            m_time = p_time;
        }
        public Vector3 GetDir()
        {
            Vector3 l_point = m_target.transform.position + (m_target.transform.forward * Mathf.Clamp(m_target.GetCurrSpeed() * m_time, 0, 100));
            return -(l_point - m_origin.position).normalized;
        }
    }
}
