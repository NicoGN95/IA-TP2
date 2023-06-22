using _Main._main.Scripts.Entities.Enemies;
using UnityEngine;

namespace _Main._main.Scripts.FSM.Base
{

    public abstract class StateCondition : ScriptableObject
    {
        public abstract bool CompleteCondition(EnemyModel p_model);
    }
}