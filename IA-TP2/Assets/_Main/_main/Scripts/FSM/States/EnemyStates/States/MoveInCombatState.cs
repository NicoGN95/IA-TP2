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
            var l_rndVector = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
            m_datas[p_model] = l_rndVector.normalized;
            
            
            var l_rnd = Random.Range(p_model.GetData().MoveInCombatTimeRange[0],
                p_model.GetData().MoveInCombatTimeRange[1]);
            
            p_model.SbController.SetZeroSb();
            p_model.SetTimeToEndAction(l_rnd);
            
            //Seleccionar la direccion del movimiento respecto al enemigo
            // izq, der, adelante, atras
        }

        public override void ExecuteState(EnemyModel p_model)
        {
            //Mover al enemy en la direccion deseada mientras esta mirando al player
            p_model.Move(m_datas[p_model].normalized, p_model.GetData().CombatMovementSpeed);
        }

        public override void ExitState(EnemyModel p_model)
        {
            m_datas.Remove(p_model);
        }
    }
}