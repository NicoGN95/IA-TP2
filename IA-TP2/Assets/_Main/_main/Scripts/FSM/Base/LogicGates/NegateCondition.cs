using _Main._main.Scripts.Entities.Enemies;
using UnityEngine;

namespace _Main._main.Scripts.FSM.Base.LogicGates
{
    [CreateAssetMenu(fileName = "AndCondition", menuName = "_main/States/Conditions/Logic/NEGATE")]
    public class NegateCondition : StateCondition
    {
        [SerializeField] private StateCondition condition;
        public override bool CompleteCondition(EnemyModel model)
        {
            return !condition.CompleteCondition(model);
        }
    }
}