using _Main._main.Scripts.Datas;
using _Main._main.Scripts.Entities.Enemies;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Main._main.Scripts.Classes
{
    public class ObstacleAvoidance
    {
        private readonly EnemyModel m_model;
        private readonly Transform m_self;

        private readonly EnemyData m_data;

        private readonly Collider[] m_obstaclesColliders;
        private readonly LayerMask m_obstaclesMask;

        public ObstacleAvoidance(EnemyModel p_model)
        {
            m_model = p_model;
            m_self = p_model.transform;

            var l_data = m_model.GetData();
            Assert.IsNotNull(l_data);

            m_data = l_data;
            m_obstaclesColliders = new Collider[l_data.MaxObjectsDetection];
            m_obstaclesMask = l_data.ObsMask;
        }

        public Vector3 GetDir(Vector3 p_targetPosition)
        {
            var l_checkRadius = m_data.CheckRadiusNearObstacles;
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
                                  m_data.BehaviourIntensity;

            l_dir += l_dirObstacleToSelf;

            return l_dir.normalized;
        }
    }
}