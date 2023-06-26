using System.Collections.Generic;
using _Main._main.Scripts.Entities.Enemies;
using _Main._main.Scripts.FSM.Base;
using UnityEngine;

namespace _Main._main.Scripts.FSM.States.EnemyStates.States
{
    [CreateAssetMenu(fileName = "SearchState", menuName = "_main/States/EnemyStates/SearchState", order = 0)]
    public class SearchState : State
    {
        private class SearchData
        {
            public SearchData(Vector3 p_point, float p_waitTime)
            {
                Point = p_point;
                WaitTime = p_waitTime;
            }
            public Vector3 Point;
            public float WaitTime;
        }
        
        private Dictionary<EnemyModel, SearchData> m_dictionary = new Dictionary<EnemyModel, SearchData>();
        
        public override void EnterState(EnemyModel p_model)
        {
            m_dictionary[p_model] = new SearchData(p_model.LastKnownTargetLocation, 3);
            p_model.SetTimeToEndSearch(p_model.GetData().SearchTime);
            p_model.SbController.SetZeroSb();
        }

        public override void ExecuteState(EnemyModel p_model)
        {
            var l_diffToTarget = m_dictionary[p_model].Point - p_model.transform.position;
            if (l_diffToTarget.magnitude < 1f)
            {
                m_dictionary[p_model].WaitTime -= Time.deltaTime;
                
                if(m_dictionary[p_model].WaitTime > 0)
                    return;
                
                
                Vector3 l_rndVector = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
                m_dictionary[p_model].Point = p_model.transform.position + l_rndVector;
                m_dictionary[p_model].WaitTime = 3;
            }
            p_model.Move(m_dictionary[p_model].Point, p_model.GetData().WalkSpeed);
        }

        public override void ExitState(EnemyModel p_model)
        {
            m_dictionary.Remove(p_model);
        }
    }
}