using _Main._main.Scripts.Entities.Enemies;
using _Main._main.Scripts.FSM.Base;
using UnityEngine;

namespace _Main._main.Scripts.FSM.States.EnemyStates.States
{
    [CreateAssetMenu(fileName = "ChaseState", menuName = "_main/States/EnemyStates/ChaseState", order = 0)]
    public class ChaseState : State
    {
        public override void EnterState(EnemyModel p_model)
        {
            p_model.SbController.SetPursuitSb();
        }

        public override void ExecuteState(EnemyModel p_model)
        {
            var l_diff = p_model.LastKnownTargetLocation - p_model.transform.position;

            var l_dir = l_diff.normalized;
            p_model.Move(l_dir, p_model.GetData().RunSpeed);
        }

        public override void ExitState(EnemyModel p_model)
        {
        }
    }
}