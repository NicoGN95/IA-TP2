using _Main._main.Scripts.Entities.Enemies;
using _Main._main.Scripts.FSM.Base;
using UnityEngine;

namespace _Main._main.Scripts.FSM.States.EnemyStates.Conditions
{
    [CreateAssetMenu(fileName = "InsideCombatAreaCondition", menuName = "_main/States/Conditions/InsideCombatAreaCondition", order = 0)]
    public class InsideCombatAreaCondition : StateCondition
    {
        public override bool CompleteCondition(EnemyModel model)
        {
            var l_distanceToTarget = Vector3.Distance(model.GetTargetTransform().position, model.transform.position);
            return l_distanceToTarget < model.GetData().CombatRange;
        }
    }
}