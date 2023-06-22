using _Main._main.Scripts.Entities.Enemies;
using _Main._main.Scripts.FSM.Base;
using UnityEngine;

namespace _Main._main.Scripts.FSM.States.EnemyStates.States
{
    [CreateAssetMenu(fileName = "BlockState", menuName = "_main/States/EnemyStates/BlockState", order = 0)]
    public class BlockState : State
    {
        public override void EnterState(EnemyModel p_model)
        {
            p_model.SetBlocking(true);

            var l_rnd = Random.Range(p_model.GetData().BlockTimeRange[0], p_model.GetData().BlockTimeRange[1]);
            p_model.SetTimeToEndAction(l_rnd);
        }

        public override void ExecuteState(EnemyModel p_model)
        {
        }

        public override void ExitState(EnemyModel p_model)
        {
            p_model.SetBlocking(false);
        }
    }
}