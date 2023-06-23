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
            public SearchData(Vector3 p_target, float p_waitTime)
            {
                Target = p_target;
                WaitTime = p_waitTime;
            }
            public Vector3 Target;
            public float WaitTime;
        }
        private Dictionary<EnemyModel, SearchData> m_dictionary = new Dictionary<EnemyModel, SearchData>();
        
        public override void EnterState(EnemyModel p_model)
        {
            m_dictionary[p_model] = new SearchData(p_model.LastKnownTargetLocation, 5);
            p_model.SetTimeToEndSearch(p_model.GetData().SearchTime);
            p_model.SbController.SetPursuitSb();
        }

        public override void ExecuteState(EnemyModel p_model)
        {
            p_model.Move(m_dictionary[p_model].Target, p_model.GetData().WalkSpeed);
            
            var l_diffToTarget = m_dictionary[p_model].Target - p_model.transform.position;
            if (l_diffToTarget.magnitude < 1f)
            {
                p_model.Move(Vector3.zero, 0);
                m_dictionary[p_model].WaitTime -= Time.deltaTime;
                
                if(m_dictionary[p_model].WaitTime > 0)
                    return;
                
                m_dictionary[p_model].WaitTime = 5;
                
                //Todo, este random deveria ser un random de la grilla
                Vector3 l_rndVector = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
                m_dictionary[p_model].Target = l_rndVector;
            }
        }

        public override void ExitState(EnemyModel p_model)
        {
            m_dictionary.Remove(p_model);
        }
    }
}