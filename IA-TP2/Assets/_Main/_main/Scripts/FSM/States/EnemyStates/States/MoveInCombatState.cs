using System.Collections.Generic;
using _Main._main.Scripts.Entities.Enemies;
using _Main._main.Scripts.FSM.Base;
using UnityEngine;

namespace _Main._main.Scripts.FSM.States.EnemyStates.States
{

    [CreateAssetMenu(fileName = "MoveInCombatState", menuName = "_main/States/EnemyStates/MoveInCombatState",
        order = 0)]
    public class MoveInCombatState : State
    {

        private Dictionary<EnemyModel, Vector3> m_datas = new Dictionary<EnemyModel, Vector3>();

        public override void EnterState(EnemyModel p_model)
        {
            Debug.Log("Move");
            var l_rndVector = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            m_datas[p_model] = l_rndVector + p_model.transform.position;
            
            var l_rndTimer = Random.Range(p_model.GetData().MoveInCombatTimeRange[0],
                p_model.GetData().MoveInCombatTimeRange[1]);
            
            p_model.SbController.SetZeroSb();
            p_model.SetTimeToEndAction(l_rndTimer);
        }

        public override void ExecuteState(EnemyModel p_model)
        {
            p_model.MoveInCombat(m_datas[p_model], p_model.GetData().CombatMovementSpeed);
            p_model.LookAt(p_model.LastKnownTargetLocation);
        }

        public override void ExitState(EnemyModel p_model)
        {
            m_datas.Remove(p_model);
        }
    }
}