using _Main._main.Scripts.Entities.Enemies;
using _Main._main.Scripts.FSM.Base;
using UnityEngine;

namespace _Main._main.Scripts.FSM.States.EnemyStates.Conditions
{
    [CreateAssetMenu(fileName = "IsPerformingAction", menuName = "_main/States/Conditions/IsPerformingAction", order = 0)]
    public class IsPerformingAction : StateCondition
    {
        public override bool CompleteCondition(EnemyModel p_model)
        {
            return p_model.GetTimeToEndAction() >= Time.time;
        }
    }
}