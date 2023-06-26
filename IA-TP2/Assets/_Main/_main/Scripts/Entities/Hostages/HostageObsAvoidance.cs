using UnityEngine;

namespace _Main._main.Scripts.Entities.Hostages
{
    public class HostageObsAvoidance
    {
        private readonly Transform m_self;
        private float m_radius;
        private float m_behaviorIntensity;

        private readonly Collider[] m_obstaclesColliders;
        private readonly LayerMask m_obstaclesMask;

        public HostageObsAvoidance(int p_maxObjectsDetection, LayerMask p_obstaclesMask, Transform p_transform, float p_radius, float p_behaviorIntensity)
        {
            m_self = p_transform;
            m_radius = p_radius;
            m_behaviorIntensity = p_behaviorIntensity;
            
            m_obstaclesColliders = new Collider[p_maxObjectsDetection];
            m_obstaclesMask = p_obstaclesMask;
        }

        public Vector3 GetDir(Vector3 p_targetPosition)
        {
            var l_checkRadius = m_radius;
            var l_selfPosition = m_self.position;
            var l_dir = (p_targetPosition - l_selfPosition);

            var l_countObjs = Physics.OverlapSphereNonAlloc(l_selfPosition, l_checkRadius, m_obstaclesColliders, m_obstaclesMask);

            Collider l_nearestObject = null;
            float l_distanceNearObj = 0;

            for (var l_i = 0; l_i < l_countObjs; l_i++)
            {
                var l_curr = m_obstaclesColliders[l_i];
            
                if (l_selfPosition == l_curr.transform.position) 
                    continue;
            
                var l_closestPointToSelf = l_curr.ClosestPointOnBounds(l_selfPosition);
                var l_distanceCurr = Vector3.Distance(l_selfPosition, l_closestPointToSelf);

                if (l_nearestObject == null)
                {
                    l_nearestObject = l_curr;
                    l_distanceNearObj = l_distanceCurr;
                }
                else
                {
                    var l_distance = Vector3.Distance(l_selfPosition, l_curr.transform.position);

                    if (!(l_distanceNearObj > l_distance))
                        continue;

                    l_nearestObject = l_curr;
                    l_distanceNearObj = l_distanceCurr;
                }
            }

            if (l_nearestObject == default)
                return l_dir.normalized;

            var l_posObj = l_nearestObject.transform.position;
            var l_dirObstacleToSelf = l_selfPosition - l_posObj;

            l_dirObstacleToSelf = l_dirObstacleToSelf.normalized * ((l_checkRadius - l_distanceNearObj) / l_checkRadius) *
                                  m_behaviorIntensity;

            l_dir += l_dirObstacleToSelf;

            return l_dir.normalized;
        }
    }
}