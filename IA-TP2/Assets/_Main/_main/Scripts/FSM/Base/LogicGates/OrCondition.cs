using _Main._main.Scripts.Entities.Enemies;
using UnityEngine;

namespace _Main._main.Scripts.FSM.Base.LogicGates
{
    [CreateAssetMenu(fileName = "AndCondition", menuName = "_main/States/Conditions/Logic/OR")]
    public class OrCondition : StateCondition
    {
        [SerializeField] private StateCondition conditionOne;
        [SerializeField] private StateCondition conditionTwo;
        public override bool CompleteCondition(EnemyModel model)
        {
            return conditionOne.CompleteCondition(model) || conditionTwo.CompleteCondition(model);
        }
    }
}