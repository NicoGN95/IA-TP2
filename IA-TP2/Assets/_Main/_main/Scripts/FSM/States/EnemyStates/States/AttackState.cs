using _Main._main.Scripts.Entities.Enemies;
using _Main._main.Scripts.FSM.Base;
using UnityEngine;

namespace _Main._main.Scripts.FSM.States.EnemyStates.States
{
    [CreateAssetMenu(fileName = "AttackState", menuName = "_main/States/EnemyStates/AttackState", order = 0)]
    public class AttackState : State
    {
        public override void EnterState(EnemyModel p_model)
        {
            /*
            var l_attack = p_model.EnemyActionController.GetAttackFromRoulette();
            p_model.SetTimeToEndAction(l_attack.AttackData.AnimationLength);
            p_model.InitAttack(l_attack);
            */
        }

        public override void ExecuteState(EnemyModel p_model)
        {
            p_model.ExecuteAttack();
        }

        public override void ExitState(EnemyModel p_model)
        {
        }
    }
}