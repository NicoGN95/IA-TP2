using _Main._main.Scripts.Entities.Enemies;
using _Main._main.Scripts.FSM.Base;
using UnityEngine;

namespace _Main._main.Scripts.FSM.States.EnemyStates.Conditions
{
    [CreateAssetMenu(fileName = "IsAlertCondition", menuName = "_main/States/Conditions/IsAlertCondition", order = 0)]
    public class IsAlertCondition : StateCondition
    {
        public override bool CompleteCondition(EnemyModel p_model)
        {
            return p_model.GetTimeToEndAlert() > Time.time;
        }
    }
}