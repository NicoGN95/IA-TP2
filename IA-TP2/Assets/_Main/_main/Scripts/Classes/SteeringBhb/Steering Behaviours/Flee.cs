using UnityEngine;

namespace _Main._main.Scripts.Classes.SteeringBhb.Steering_Behaviours
{
    public class Flee : ISteeringBehaviour
    {
        private Transform m_origin;
        private Transform m_target;
        
        public Flee(Transform p_origin, Transform p_target)
        {
            m_origin = p_origin;
            m_target = p_target;
        }
        
        
        public Vector3 GetDir()
        {
            return -(m_target.position - m_origin.position).normalized;
        }
    }
}
