using _Main._main.Scripts.Entities.Enemies;
using _Main._main.Scripts.FSM.Base;
using UnityEngine;

namespace _Main._main.Scripts.FSM.States.EnemyStates.States
{
    [CreateAssetMenu(fileName = "RunAwayFromPlayerState", menuName = "_main/States/EnemyStates/RunAwayFromPlayerState", order = 0)]
    public class RunAwayFromPlayerState : State
    {
        
        public override void EnterState(EnemyModel p_model)
        {
            p_model.SbController.SetFleeSb();
        }

        public override void ExecuteState(EnemyModel p_model)
        {
            var l_playerDir = (p_model.GetTargetTransform().position - p_model.transform.position).normalized;
            
            p_model.Move(-l_playerDir, p_model.GetData().RunSpeed);
        }
    }
}