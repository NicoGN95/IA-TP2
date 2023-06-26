using System.Collections.Generic;
using _Main._main.Scripts.Entities.Enemies;
using _Main._main.Scripts.FSM.Base;
using UnityEngine;

namespace _Main._main.Scripts.FSM.States.EnemyStates.States
{
    [CreateAssetMenu(fileName = "PatrolState", menuName = "_main/States/EnemyStates/PatrolState", order = 0)]
    public class PatrolState : State
    {
        private class PatrolData
        {
            public int WaypointCount;
            public float WaitTime; 
            public Vector3 TargetPatrolPoint;
        }
        private Dictionary<EnemyModel, PatrolData> m_patrolDatas = new Dictionary<EnemyModel, PatrolData>();
        public override void EnterState(EnemyModel p_model)
        {
            m_patrolDatas[p_model] = new PatrolData();
            
            m_patrolDatas[p_model].TargetPatrolPoint = p_model.PatrolPoints[0];
            m_patrolDatas[p_model].WaitTime = p_model.GetData().WaitPatrolTime;
            
            p_model.SbController.SetZeroSb();
        }

        public override void ExecuteState(EnemyModel p_model)
        {
            var l_diff = (m_patrolDatas[p_model].TargetPatrolPoint - p_model.transform.position);

            if (l_diff.magnitude < 1)
            {
                m_patrolDatas[p_model].WaitTime -= Time.deltaTime;

                if (m_patrolDatas[p_model].WaitTime >= 0)
                {
                    p_model.Move(Vector3.zero, 0);
                    return;
                }
                
                
                //ya llego al punto, necesita ir al siguiente
                m_patrolDatas[p_model].WaypointCount++;
                
                if (m_patrolDatas[p_model].WaypointCount >= p_model.PatrolPoints.Lenght)
                {
                    m_patrolDatas[p_model].WaypointCount = 0;
                }

                m_patrolDatas[p_model].TargetPatrolPoint = p_model.PatrolPoints[m_patrolDatas[p_model].WaypointCount];
                m_patrolDatas[p_model].WaitTime = p_model.GetData().WaitPatrolTime;
            }

            p_model.Move(m_patrolDatas[p_model].TargetPatrolPoint, p_model.GetData().WalkSpeed);
        }

        public override void ExitState(EnemyModel p_model)
        {
            m_patrolDatas.Remove(p_model);
        }
    }
}