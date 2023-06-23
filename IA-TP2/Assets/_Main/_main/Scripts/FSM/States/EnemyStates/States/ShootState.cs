using _Main._main.Scripts.Entities.Enemies;
using _Main._main.Scripts.FSM.Base;
using UnityEngine;

namespace _Main._main.Scripts.FSM.States.EnemyStates.States
{
    [CreateAssetMenu(fileName = "ShootState", menuName = "_main/States/EnemyStates/ShootState", order = 0)]
    public class ShootState : State
    {
        public override void EnterState(EnemyModel p_model)
        {
            p_model.SetTimeToEndAction(p_model.GetData().ShootTime);
            p_model.Shoot();
            p_model.SbController.SetZeroSb();
        }

        public override void ExecuteState(EnemyModel p_model)
        {
            p_model.Move(Vector3.zero, 0f);
        }

        public override void ExitState(EnemyModel p_model)
        {
        }
    }
}