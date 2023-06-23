using _Main._main.Scripts.Entities.Enemies;
using _Main._main.Scripts.FSM.Base;
using UnityEngine;

namespace _Main._main.Scripts.FSM.States.EnemyStates.States
{
    [CreateAssetMenu(fileName = "TakeCover", menuName = "_main/States/EnemyStates/TakeCover", order = 0)]
    public class TakeCover : State
    {
        public override void EnterState(EnemyModel p_model)
        {
            p_model.SbController.SetPursuitSb();
        }

        public override void ExecuteState(EnemyModel p_model)
        {
            throw new System.NotImplementedException();
        }
    }
}