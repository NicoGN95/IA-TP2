using _Main._main.Scripts.Entities.Enemies;
using _Main._main.Scripts.FSM.Base;
using UnityEngine;

namespace _Main._main.Scripts.FSM.States.EnemyStates.States
{
    [CreateAssetMenu(fileName = "IdleState", menuName = "_main/States/EnemyStates/IdleState", order = 0)]
    public class IdleState : State
    {
        public override void EnterState(EnemyModel p_model)
        {
            p_model.View.PlayIdleAnimation();
        }

        public override void ExecuteState(EnemyModel p_model){}
        public override void ExitState(EnemyModel p_model){}
    }
}